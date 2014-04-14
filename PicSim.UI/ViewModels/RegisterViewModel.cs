using PicSim.Components.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Foundation;

namespace PicSim.UI.ViewModels
{
    public class RegisterViewModel : Model
    {
        private readonly IRegister m_register;

        public RegisterViewModel(IRegister register)
        {
            Ensure.ArgumentNotNull(register, "register");
            this.m_register = register;
            this.m_register.RegisterChanged += (sender, e) => RaisePropertyChanged("Value");
        }

        public byte Value
        {
            get { return m_register.Value; }
            set
            {
                // TODO Define something like IRegister.RawValue
                m_register.Value = value;
            }
        }
    }
}
