using ASP_ConsoAPI_IA.Models;
using ASP_ConsoAPI_IA.Utils;
using Dal.Interface;
using Dal.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_ConsoAPI_IA.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly SessionManager _sessionManager;

        public MessageController(IMessageService messageService, SessionManager sessionManager, IUserService userService)
        {
            _messageService = messageService;
            _sessionManager = sessionManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            IEnumerable<Message> messages = _messageService.GetAll();
            List<MessageViewModel> messagesViewModel = new List<MessageViewModel>();
            foreach(Message m in messages)
            {
                messagesViewModel.Add(
                    new MessageViewModel
                    {
                        Content = m.Content,
                        CreatedAt = m.CreatedAt,
                        Pseudo = _userService.GetById(m.Member_Id).Pseudo
                    });
            }

            return View(messagesViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(NewMessageViewModel form)
        {
            if (!ModelState.IsValid) return View(form);
            _messageService.Post(form.Content, _sessionManager.CurrentUser.Token);
            return RedirectToAction("Index");
        }

    }
}
