using DevExpress.CodeParser;
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

        public static Form CreateForm(string formName,string path)
        {
            //HideFormAll(formName);
            formName = formName == null ? "FrmNone" : formName;
            Form form = forms.Find(m => m.Name == formName);
   
            Type type = types.Find(m => m.Name == formName);
            object[] parameters = new object[] { path };
            if (formName.Equals("FrmFolder"))
            {
                form = (Form)Activator.CreateInstance(type, path);
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
    }
}
