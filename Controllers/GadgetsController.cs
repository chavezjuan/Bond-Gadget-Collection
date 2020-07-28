using BondGadgetCollection.Data;
using BondGadgetCollection.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BondGadgetCollection.Controllers
{
    public class GadgetsController : Controller
    {
        // GET: Gadgets
        public ActionResult Index()
        {
            List<GadgetModel> gadgets = new List<GadgetModel>();

            GadgetDAO gadgetDAO = new GadgetDAO();

            gadgets = gadgetDAO.FetchAll();

            return View("Index", gadgets);
        }

        public ActionResult Details(int id)
        {
            GadgetDAO gadgetDAO = new GadgetDAO();
            GadgetModel gadget = gadgetDAO.FetchOne(id);

            return View("Details", gadget);
        }

        //Retorna um formulário de criação
        public ActionResult Create()
        {
            return View("GadgetForm");
        }

        public ActionResult Edit(int id)
        {
            GadgetDAO gadgetDAO = new GadgetDAO();
            GadgetModel gadget = gadgetDAO.FetchOne(id);
            return View("GadgetForm", gadget);
        }

        public ActionResult Delete(int id)
        {
            GadgetDAO gadgetDAO = new GadgetDAO();
            gadgetDAO.Delete(id);

            List<GadgetModel> gadgets = gadgetDAO.FetchAll();

            return View("Index", gadgets);
        }

        //Cria um item no banco
        public ActionResult ProcessCreate(GadgetModel gadgetModel)
        {
            //Salvando no banco de dados
            GadgetDAO gadgetDAO = new GadgetDAO();

            gadgetDAO.CreateOrUpdate(gadgetModel);

            return View("Details", gadgetModel);
        }

        public ActionResult SearchForm()
        {
            return View("SearchForm");
        }

        public ActionResult SearchForName(string searchPhrase)
        {
            GadgetDAO gadgetDAO = new GadgetDAO();
            List<GadgetModel> searchResults = gadgetDAO.SearchForName(searchPhrase);


            return View("Index", searchResults);
        }
    }
}