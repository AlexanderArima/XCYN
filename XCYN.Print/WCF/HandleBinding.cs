using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.WCF
{
    public class HandleBinding
    {
        public void Fun1()
        {
            var newNamedbinding = new NetNamedPipeBinding();
            var nettcpBinding = new NetTcpBinding();
            var wsHttpBinding = new WSHttpBinding();
            var basicHttpBinding = new BasicHttpBinding();
            LookUpBinding(newNamedbinding);
            LookUpBinding(nettcpBinding);
            LookUpBinding(wsHttpBinding);
            LookUpBinding(basicHttpBinding);
        }

        /// <summary>
        /// 检索信道栈
        /// </summary>
        /// <param name="binding"></param>
        public void LookUpBinding(Binding binding)
        {
            Console.WriteLine("Binding:{0}",binding.GetType().Name);

            var collect = binding.CreateBindingElements();

            foreach (var item in collect)
            {
                Console.WriteLine(item.GetType().FullName);
            }
            Console.WriteLine();
        }

    }
}
