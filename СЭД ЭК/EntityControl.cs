using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace СЭД_ЭК
{
    public interface EntityControl<Type>
    {
         void FillDataTable(object sender, TabItem tabItem);
         void Show();
         void FromTableToList(object sender, TabItem tbctrl);
    }
}
