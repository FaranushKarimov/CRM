using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entities.Enums
{
    public enum Roles
    {
        [Description("Админ")]
        Admin = 1,
        [Description("Фронт")]
        Front,
        [Description("Бэк")]
        Back,
        [Description("Архив")]
        Archive
    }
}
