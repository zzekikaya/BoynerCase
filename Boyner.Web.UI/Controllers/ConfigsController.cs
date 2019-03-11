using AutoMapper;
using Boyner.Core.Repository;
using Boyner.Data;
using Boyner.Domain.Entities;
using Boyner.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.MessageBroker.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace Boyner.Web.UI.Controllers
{
    
    public class ConfigsController : Controller
    {
        private readonly ConfigContext _context;
        protected IUnitOfWork _unitOfWork { get; }
        private readonly IMapper _mapper;
        private readonly IMessageBroker _messageBroker;
        public ConfigsController(IUnitOfWork unitOfWork, IMapper mapper, IMessageBroker messageBroker)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           _messageBroker = messageBroker;
        }

        // GET: Configs
        public async Task<IActionResult> Index()
        {
            var configList = _unitOfWork.Select<Config>().ToList();
            var configListModel = Mapper.Map<List<Config>, List<ConfigModel>>(configList);
            return View(configListModel);
        }

        // GET: Configs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var config = _unitOfWork.Select<Config>().FirstOrDefault(x => x.Id == id);
            var Configmodel = _mapper.Map<Config, ConfigModel>(config);
            if (config == null)
            {
                return NotFound();
            }

            return View(Configmodel);
        }

        // GET: Configs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create([Bind("Id,Name,Type,Value,IsActive,ApplicationName")] ConfigModel config)
        {
            if (ModelState.IsValid)
            {
                var configEntity = _mapper.Map<ConfigModel, Config>(config);
                _unitOfWork.Insert(configEntity);
                _unitOfWork.Commit();
                _messageBroker.Publisher("myqueue", string.Format(" add new cofig : application name: {0} value: :{1}", config.ApplicationName));
            }
            return RedirectToAction("Index");
        }

        // GET: Configs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = _unitOfWork.Select<Config>().FirstOrDefault(x => x.Id == id);
            if (config == null)
            {
                return NotFound();
            }
            var ConfigModel = _mapper.Map<Config, ConfigModel>(config);
            return View(ConfigModel);

        }

        // POST: Configs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Type,Value,IsActive,ApplicationName")] ConfigModel config)
        {
            if (id != config.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var configEntity = _mapper.Map<ConfigModel, Config>(config);
                    _unitOfWork.Update(configEntity);
                    _unitOfWork.Commit();
                    _messageBroker.Publisher("myqueue", string.Format(" edit cofig : application name: {0} value: :{1}", config.ApplicationName));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigExists(config.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToAction("Index");

        }

        // GET: Configs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var config = _unitOfWork.Select<Config>().FirstOrDefault(x => x.Id == id);
            if (config == null)
            {
                return NotFound();
            }

            var ConfigModel = _mapper.Map<Config, ConfigModel>(config);
            return View(ConfigModel);
        }

        // POST: Configs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var config = _unitOfWork.Select<Config>().FirstOrDefault(x => x.Id == id);
            _unitOfWork.Delete(config);
            _unitOfWork.Commit();
            _messageBroker.Publisher("myqueue", string.Format(" delete cofig : application name: {0} value: :{1}", config.ApplicationName));
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index");

        }

        private bool ConfigExists(int id)
        {
            return _unitOfWork.Select<Config>().Any(x => x.Id == id);
        }
    }
}
