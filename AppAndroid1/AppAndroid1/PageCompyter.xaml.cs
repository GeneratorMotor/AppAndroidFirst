using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAndroid1.Classess;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAndroid1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCompyter : ContentPage
    {
        
        private ViewCompyter viewCompyter;
        public PageCompyter(ViewCompyter vk)
        {
            InitializeComponent();

            if(vk == null)
            {
                throw new ArgumentNullException("Отсутствует значения для описания оргтехники");
            }

            // Привяжем экземпляр типа ViewCompyter
            // К контексту страницы.
            this.BindingContext = vk;
        }
    }
}