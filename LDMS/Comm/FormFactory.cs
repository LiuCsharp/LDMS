using DevExpress.CodeParser;
using LDMS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDMS.Comm
{
    public class FormFactory
    {
        private static List<Type> types = new List<Type>();
        private static List<Form> forms = new List<Form>();

        
        static FormFactory()
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom("LDMS");
                types = assembly.GetTypes().ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        public static Form CreateForm(string formName,string path, List<FileList> fileLists)
        {
            //HideFormAll(formName);
            formName = formName == null ? "FrmNone" : formName;
            Form form = forms.Find(m => m.Name == formName);
   
            Type type = types.Find(m => m.Name == formName);
            object[] parameters = new object[] { path };
            if (formName.Equals("FrmFolder"))
            {
                form = (Form)Activator.CreateInstance(type, path, fileLists);
            }
            else 
            {
                form = (Form)Activator.CreateInstance(type);
            }
            
            forms.Add(form);                                
            return form;
        }

        public static void HideFormAll(string formName)
        {
            foreach (Form frm in forms)
            {
                if (frm.Name == formName) frm.Hide();
            }
        }


        public static string GetDay(string date)
        {
            string day = "";
            TimeSpan span = DateTime.Now - Convert.ToDateTime(date);

            if (span.TotalDays > 30)
            {
                decimal days = span.Days / 30;
                days = Math.Ceiling(days);
                day = days.ToString() + "月前";
            }
            else if (span.TotalDays > 21)
            {
                day = "3周前";
            }
            else if (span.TotalDays > 14)
            {
                day = "2周前";
            }
            else if (span.TotalDays > 7)
            {
                day = "1周前";
            }
            else if (span.TotalDays > 1)
            {
                day = string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
            }
            else if (span.TotalHours > 1)
            {
                day = "今天";
            }

            return day;

        }
    }
}
