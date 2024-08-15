using LMSweb_v3.Services;
using LMSweb_v3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Text.RegularExpressions;
using LMSwebDB.Models;


namespace LMSweb_v3.Controllers;

[Authorize(Roles = "Teacher")]
public class PromptManageController : Controller
{
    private readonly CourseService _courseService;
    private readonly ChatService _chatService;
    private object _context;

    // 构造函数
    public PromptManageController(CourseService courseService, ChatService chatService)
    {
        _courseService = courseService;
        _chatService = chatService;
    }

    // 编辑页面的Get方法
    public IActionResult Edit(string cid)
    {
        var data = _courseService.GetCourseDefaultPrompt(cid);
        return View(data);
    }

    // 编辑页面的Post方法
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(string cid, PromptManageViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // 确保没有包含不允许的系统符号
        if (Regex.IsMatch(model.Greeting, @"<!reference_data>|<!question>|<!context>"))
        {
            ModelState.AddModelError("Greeting", "不可以包含系統內定的替換符號!!");
            return View(model);
        }

        // 更新默认提示语
        var isUpdated = _courseService.UpdateDefaultPrompt(model);
        if (!isUpdated)
        {
            ViewBag.isError = true;
            ViewBag.Message = "無法更新";
            return View(model);
        }

        return RedirectToAction("Index", "Material", new { cid = model.CourseId });
    }

    // 导出聊天历史的方法
    public IActionResult Export(string cid)
    {
        var data = _chatService.GetChatHistory(cid);
        var fileName = $"chat_history_{cid}.xlsx";
        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        using var stream = new MemoryStream();
        data.SaveAs(stream);
        var content = stream.ToArray();
        stream.Flush();
        data.Dispose();

        return File(content, contentType, fileName);
    }

    [HttpPost]
    public JsonResult CreateTemplate(string templateName)
    {
        if (!string.IsNullOrEmpty(templateName))
        {
            var prompt = new LMSwebDB.Models.Prompt
            {
                Name = templateName
            };

            _courseService.AddPrompt(prompt); 

            return Json(new { success = true, templateId = prompt.Id });
        }

        return Json(new { success = false });
    }


}
