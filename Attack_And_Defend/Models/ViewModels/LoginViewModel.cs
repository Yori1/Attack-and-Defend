using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Attack_And_Defend.Models
{
    public class LoginViewModel
    {
        public string CustomErrorMessage { get; private set; } = "";
        public IEnumerable<IdentityError> IdentityErrors { get; private set; } = new List<IdentityError>();

        public LoginViewModel(string customErrorMessage)
        {
            CustomErrorMessage = customErrorMessage;
        }

        public LoginViewModel(IEnumerable<IdentityError> errors)
        {
            IdentityErrors = errors;
        }
    }
}
