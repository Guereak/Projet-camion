using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransConnect_Console
{
    class CompanyTreeNode<T>    // We leave room for templating, even though in practice we will only use it with 'Salarie'
    {
        private T manager;
        private CompanyTreeNode<T> next;
        private CompanyTreeNode<T> managees;

        #region accessors
        public T Manager 
        { 
            get { return manager; }  
            set { manager = value; } 
        }
        public CompanyTreeNode<T> Next
        {
            get { return next; }
            set { next = value; }
        } 
        public CompanyTreeNode<T> Managees
        {
            get { return managees; }
            set { managees = value; }
        }
        #endregion
    }

    class CompanyTree
    {
        CompanyTreeNode<Salarie> CEO;
    }
}
