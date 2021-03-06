using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using edit20210325.Function;
using edit20210325.Models;
using edit20210325.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace edit20210325.Controllers
{
    [Area(ProjectSet.AdminName)]
    public class CashInController : Controller
    {
        const int pageRowCount = 20;
        public IActionResult Index(string search = "", string searchSelect = "1", int page = 1, bool FuzzyOrNormal = false)
        {
            if (HttpContext.User.Claims.Count() == 0) return RedirectToAction("Index", "Login");
            var db = new CASE20210405Context();

            List<MemberCashInModel> lsSourceModels = db.MemberCashInModels.ToList();

            List<MemberModel> ModelList = db.MemberModels.ToList();

            List<MarkMemberCashInModel> lsModelout = ModelList.Join(
                lsSourceModels,
                m => Convert.ToString(m.Id),
                s => s.MemberCashInId,
                (m, s) => new MarkMemberCashInModel()
                {
                    Id = s.Id,
                    MemberEmail = m.MemberGmail,
                    MemberName = m.MemberName,
                    MemberCash = s.MemberCashInCash,
                    CaseName = s.MemberCashInName,
                    CaseRemarks = s.MemberCashInRemarks,
                    Space1 = s.MemberCashInSpace1,
                    Space2 = s.MemberCashInSpace2,
                    Space3 = s.MemberCashInSpace3,
                    CreateDate = s.MemberCashInCrtDate,
                    ChangeDate = s.MemberCashInChgDate
                }).OrderBy(c => c.Id).ToList();

            var pageCnt = page;
            var pageRows = pageRowCount;
            var querylsModel = lsModelout.Skip((pageCnt - 1) * pageRows).Take(pageRows);
            int listConut = (lsModelout.Count / pageRowCount) + 1;
            if (lsModelout.Count % pageRowCount > 0) listConut += 1;
            var pageNext = pageCnt + 1;
            if (pageNext >= listConut - 1) pageNext = listConut - 1;
            var pagePrev = pageCnt - 1;
            if (pagePrev <= 1) pagePrev = 1;
            ViewData["pageNext"] = pageNext;
            ViewData["pagePrev"] = pagePrev;
            int listConutStart = 0;
            int listConutEnd = 10;
            var pageRowsQuantity = 5;
            if (page <= pageRowsQuantity)
            {
                listConutStart = 1;
                listConutEnd = 10;
            }
            else
            {
                listConutStart = page - pageRowsQuantity;
                listConutEnd = page + pageRowsQuantity - 1;
            }
            if (listConutStart <= 1) listConutStart = 1;
            if (listConutEnd > listConut) listConutEnd = listConut - 1;
            ViewData["listConut"] = listConut;
            ViewData["page"] = page;
            ViewData["listConutStart"] = listConutStart;
            ViewData["listConutEnd"] = listConutEnd;
            ViewData["CountModel"] = querylsModel.Count();
            TempData["save"] = JsonConvert.SerializeObject(lsModelout);
            ViewData["search"] = search;
            ViewData["searchSelect"] = searchSelect;
            ViewData["FuzzyOrNormal"] = FuzzyOrNormal;
            return View(querylsModel);
        }

        public ActionResult SearchView(string search = "", string searchSelect = "1", int page = 1)
        {
            string strlsModel = (string)TempData["save"];
            List<MarkMemberCashInModel> lsModelout = JsonConvert.DeserializeObject<List<MarkMemberCashInModel>>(strlsModel);

            if (search == null) search = "";
            List<MarkMemberCashInModel> serachModelout;
            switch (searchSelect)
            {
                case "1":
                    serachModelout = lsModelout.Where(c => c.MemberEmail.Equals(search)).ToList();
                    break;
                case "2":
                    serachModelout = lsModelout.Where(c => c.MemberName.Equals(search)).ToList();
                    break;
                case "3":
                    serachModelout = lsModelout.Where(c => c.MemberCash.ToString().Equals(search)).ToList();
                    break;
                case "4":
                    serachModelout = lsModelout.Where(c => c.CaseName.Equals(search)).ToList();
                    break;
                case "5":
                    serachModelout = lsModelout.Where(c => c.CaseRemarks.Equals(search)).ToList();
                    break;
                case "6":
                    serachModelout = lsModelout.Where(c => c.Space1.Equals(search)).ToList();
                    break;
                case "7":
                    serachModelout = lsModelout.Where(c => c.Space2.Equals(search)).ToList();
                    break;
                case "8":
                    serachModelout = lsModelout.Where(c => c.Space3.Equals(search)).ToList();
                    break;
                case "9":
                    serachModelout = lsModelout.Where(c => c.CreateDate.Date.ToString("yyyy/MM/dd").Equals(search)).ToList();
                    break;
                case "10":
                    serachModelout = lsModelout.Where(c => c.ChangeDate.Date.ToString("yyyy/MM/dd").Equals(search)).ToList();
                    break;
                default:
                    serachModelout = lsModelout.Where(c => c.MemberEmail.Equals(search)).ToList();
                    break;
            }

            string JsonDataout = JsonConvert.SerializeObject(serachModelout);
            TempData["save"] = JsonDataout;
            ViewData["search"] = search;
            ViewData["searchSelect"] = searchSelect;
            ViewData["FuzzyOrNormal"] = true;
            var pageCnt = page;
            var pageRows = pageRowCount;
            var querylsModel = serachModelout.Skip((pageCnt - 1) * pageRows).Take(pageRows);
            int listConut = (serachModelout.Count / pageRowCount) + 1;
            if (serachModelout.Count % pageRowCount > 0) listConut += 1;
            var pageNext = pageCnt + 1;
            if (pageNext >= listConut - 1) pageNext = listConut - 1;
            var pagePrev = pageCnt - 1;
            if (pagePrev <= 1) pagePrev = 1;
            ViewData["pageNext"] = pageNext;
            ViewData["pagePrev"] = pagePrev;
            int listConutStart = 0;
            int listConutEnd = 10;
            var pageRowsQuantity = 5;
            if (page <= pageRowsQuantity)
            {
                listConutStart = 1;
                listConutEnd = 10;
            }
            else
            {
                listConutStart = page - pageRowsQuantity;
                listConutEnd = page + pageRowsQuantity - 1;
            }
            if (listConutStart <= 1) listConutStart = 1;
            if (listConutEnd > listConut) listConutEnd = listConut - 1;
            ViewData["listConut"] = listConut;
            ViewData["page"] = page;
            ViewData["listConutStart"] = listConutStart;
            ViewData["listConutEnd"] = listConutEnd;
            ViewData["CountModel"] = querylsModel.Count();
            return View(querylsModel);
        }

        public ActionResult FuzzySearchView(string search = "", string searchSelect = "1", int page = 1)
        {

            string strlsModel = (string)TempData["save"];
            List<MarkMemberCashInModel> lsModelout = JsonConvert.DeserializeObject<List<MarkMemberCashInModel>>(strlsModel);

            if (search == null) search = "";
            List<MarkMemberCashInModel> serachModelout;
            switch (searchSelect)
            {
                case "1":
                    serachModelout = lsModelout.Where(c => c.MemberEmail.IndexOf(search) > -1).ToList();
                    break;
                case "2":
                    serachModelout = lsModelout.Where(c => c.MemberName.IndexOf(search) > -1).ToList();
                    break;
                case "3":
                    serachModelout = lsModelout.Where(c => c.MemberCash.ToString().IndexOf(search) > -1).ToList();
                    break;
                case "4":
                    serachModelout = lsModelout.Where(c => c.CaseName.IndexOf(search) > -1).ToList();
                    break;
                case "5":
                    serachModelout = lsModelout.Where(c => c.CaseRemarks.IndexOf(search) > -1).ToList();
                    break;
                case "6":
                    serachModelout = lsModelout.Where(c => c.Space1.IndexOf(search) > -1).ToList();
                    break;
                case "7":
                    serachModelout = lsModelout.Where(c => c.Space2.IndexOf(search) > -1).ToList();
                    break;
                case "8":
                    serachModelout = lsModelout.Where(c => c.Space3.IndexOf(search) > -1).ToList();
                    break;
                case "9":
                    serachModelout = lsModelout.Where(c => c.CreateDate.Date.ToString("yyyy/MM/dd").IndexOf(search) > -1).ToList();
                    break;
                case "10":
                    serachModelout = lsModelout.Where(c => c.ChangeDate.Date.ToString("yyyy/MM/dd").IndexOf(search) > -1).ToList();
                    break;
                default:
                    serachModelout = lsModelout.Where(c => c.MemberEmail.IndexOf(search) > -1).ToList();
                    break;
            }

            string JsonDataout = JsonConvert.SerializeObject(serachModelout);
            TempData["save"] = JsonDataout;
            ViewData["search"] = search;
            ViewData["searchSelect"] = searchSelect;
            ViewData["FuzzyOrNormal"] = false;
            var pageCnt = page;
            var pageRows = pageRowCount;
            var querylsModel = serachModelout.Skip((pageCnt - 1) * pageRows).Take(pageRows);
            int listConut = (serachModelout.Count / pageRowCount) + 1;
            if (serachModelout.Count % pageRowCount > 0) listConut += 1;
            var pageNext = pageCnt + 1;
            if (pageNext >= listConut - 1) pageNext = listConut - 1;
            var pagePrev = pageCnt - 1;
            if (pagePrev <= 1) pagePrev = 1;
            ViewData["pageNext"] = pageNext;
            ViewData["pagePrev"] = pagePrev;
            int listConutStart = 0;
            int listConutEnd = 10;
            var pageRowsQuantity = 5;
            if (page <= pageRowsQuantity)
            {
                listConutStart = 1;
                listConutEnd = 10;
            }
            else
            {
                listConutStart = page - pageRowsQuantity;
                listConutEnd = page + pageRowsQuantity - 1;
            }
            if (listConutStart <= 1) listConutStart = 1;
            if (listConutEnd > listConut) listConutEnd = listConut - 1;
            ViewData["listConut"] = listConut;
            ViewData["page"] = page;
            ViewData["listConutStart"] = listConutStart;
            ViewData["listConutEnd"] = listConutEnd;
            ViewData["CountModel"] = querylsModel.Count();
            return View(querylsModel);
        }

        public RedirectToActionResult exit()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var db = new CASE20210405Context();
            List<MemberModel> ModelList = db.MemberModels.ToList();
            ViewBag.ModelList = ModelList;
            return View();
        }
        [HttpPost]
        public RedirectToActionResult Create(MemberCashInModel it)
        {
            if (string.IsNullOrEmpty(it.MemberCashInId))
                return RedirectToAction("Index");
            var db = new CASE20210405Context();
            it.MemberCashInSpace1 = it.MemberCashInSpace1 == null ? "待繳款" : it.MemberCashInSpace1;
            db.MemberCashInModels.Add(it);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public RedirectToActionResult Delete(Guid? Id)
        {
            var db = new CASE20210405Context();
            MemberCashInModel it = db.MemberCashInModels.Find(Id);
            db.MemberCashInModels.Remove(it);
            var DepositOrderLogs = db.DepositOrderLogModels.Where(c => c.CashInId == Id.ToString()).ToList();
            db.DepositOrderLogModels.RemoveRange(DepositOrderLogs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid? Id)
        {
            if (Id == null) return RedirectToAction("Index");

            var db = new CASE20210405Context();
            List<MemberCashInModel> lsModel = db.MemberCashInModels.ToList();
            List<MemberCashInModel> ls = lsModel.Where(c => c.Id == Id).ToList();
            if (ls.Count == 0) return RedirectToAction("Index");
            MemberCashInModel it = ls.FirstOrDefault();

            List<MemberModel> memberList = db.MemberModels.Where(c => c.Id.ToString() == it.MemberCashInId).ToList();
            if (memberList.Count == 0) return RedirectToAction("Index");
            var member = memberList.FirstOrDefault();
            ViewBag.member = member;

            return View(it);
        }

        [HttpPost]
        public RedirectToActionResult Edit(MemberCashInModel it)
        {
            var db = new CASE20210405Context();

            if (ModelState.IsValid)
            {
                db.Entry(it).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
