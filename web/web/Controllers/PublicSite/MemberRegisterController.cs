﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Entity.Dto;
using Web.Entity.Model;
using Web.Services.Mapping;
using Web.Services.Services;
using Web.Services.Services.Members;

namespace web.Controllers.PublicSite
{
    public class MemberRegisterController : PublicBaseController
    {
        private readonly IAdministrationService _administrationService;
        private readonly IMemberService _memberService;
        public MemberRegisterController(IAdministrationService administrationService, IMemberService memberService)
        {
            _administrationService = administrationService;
            _memberService = memberService;
        }
        [Route("~/MemberRegistration")]
        public ActionResult MemberRegistration()
        {
            var obj = new MemberDto();
            return View(obj);
        }

        [HttpPost]
        public async Task<JsonResult> PostPersonalInfo(MemberPersonalInfoDto dto)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePairDto> errors = ModelState.Select(a=>a.ToModelState()).ToList();
                var errorResult = new { status = "error",errorData=errors };
                return Json(errorResult, JsonRequestBehavior.AllowGet);
            }
            var personalInfoValidation = await _memberService.ValidatePersonalInfo(dto);
            if(personalInfoValidation.Count()>0)
            {
                var errorResult = new { status = "error", errorData = personalInfoValidation };
                return Json(errorResult, JsonRequestBehavior.AllowGet);
            }
            var insertData = await _memberService.InsertUpdatePersonalInfo(dto);
            Session["MemberId"] = insertData;
            var successResult = new { status = "success", successData = insertData };
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost]
        public async Task<JsonResult> ContactInfo(MemberContactInfoDto dto)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePairDto> errors = ModelState.Select(a => a.ToModelState()).ToList();
                var errorResult = new { status = "error", errorData = errors };
                return Json(errorResult, JsonRequestBehavior.AllowGet);
            }
            var contactInfoValidation = await _memberService.ValidateContactInfo(dto);
            if (contactInfoValidation.Count() > 0)
            {
                var errorResult = new { status = "error", errorData = contactInfoValidation };
                return Json(errorResult, JsonRequestBehavior.AllowGet);
            }
            var data = await _memberService.AddContactInfo(dto);
            Session["MemberId"] = data;
            var successResult = new { status = "success", successData = data };
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetMemberById(int id)
        {
            var obj =await _memberService.GetMemberByIdAsync(id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddModifyAddress(MemberAddressDto dto)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePairDto> errors = ModelState.Select(a => a.ToModelState()).ToList();
                var errorResult = new { status = "error", errorData = errors };
                return Json(errorResult, JsonRequestBehavior.AllowGet);
            }
          
            var data = await _memberService.AddModifyMemberAddress(dto);
            Session["MemberId"] = data;
            var successResult = new { status = "success", successData = data };
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> MemberOccupation(MemberOccupationDto dto)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePairDto> errors = ModelState.Select(a => a.ToModelState()).ToList();
                var errorResult = new { status = "error", errorData = errors };
                return Json(errorResult, JsonRequestBehavior.AllowGet);
            }

            var data = await _memberService.AddOccupation(dto);
            Session["MemberId"] = data;
            var successResult = new { status = "success", successData = data };
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> MemberDocument(MemberDocumentsDto dto)
        {
            if (!ModelState.IsValid)
            {
                List<KeyValuePairDto> errors = ModelState.Select(a => a.ToModelState()).ToList();
                var errorResult = new { status = "error", errorData = errors };
                return Json(errorResult, JsonRequestBehavior.AllowGet);
            }

            var data = await _memberService.AddMemberDocument(dto);
            Session["MemberId"] = data;
            var successResult = new { status = "success", successData = data };
            return Json(successResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetMemberAddress(int memberId)
        {
            var obj = await _memberService.GetMemberAddressAsync(memberId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetMemberDocuments(int memberId)
        {
            var obj = await _memberService.GetMemberDocumentAsync(memberId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}