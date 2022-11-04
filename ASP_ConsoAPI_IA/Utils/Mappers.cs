using ASP_ConsoAPI_IA.Models;
using Dal.Models;

namespace ASP_ConsoAPI_IA.Utils
{
    public static class Mappers
    {
        public static NewUserModel ToDal(this AuthRegisterViewModel form)
        {
            return new NewUserModel
            {
                Email = form.Email,
                Password = form.Password,
                Pseudo = form.Pseudo,
                ConfirmPassword = form.ConfirmPassword
            };
        }
    }
}
