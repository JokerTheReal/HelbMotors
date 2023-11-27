using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReference.Services
{
    public partial class DeviceOrientationServices //Doit être partial car le contenu sera défini dans des dossiers spécifiques suivant la plateforme (mobile, pc, etc.)
    {

        public QueueBuffer SerialBuffer;
        public DeviceOrientationServices() { }

        public partial void ConfigureScanner(); //Doit être partial
        public class QueueBuffer : Queue
        {
            public event EventHandler Changed;

            protected virtual void OnChanged()
            {
                if (Changed != null) Changed(this, EventArgs.Empty);
            }

            public override void Enqueue(object item)
            {
                base.Enqueue(item);
                OnChanged();
            }
        }

    }
}
