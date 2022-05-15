using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_calculator
{
    class CustomRecord
    {
        string expression;
        string preorder;
        string postorder;
        double decimals;
        int binarys;
        public CustomRecord(string exp,  string pre, string post,  double dec, int bin)
        {
            expression = exp;
            preorder = pre;
            postorder = post;
            decimals = dec;
            binarys = bin;
        }
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }
        public string Preorder
        {
            get { return preorder; }
            set { preorder = value; }
        }
        public string Postorder
        {
            get { return postorder; }
            set { postorder = value; }
        }
        public double Deci
        {
            get { return decimals; }
            set { decimals = value; }
        }
        public int Bin
        {
            get { return binarys; }
            set { binarys = value; }
        }
    }
}
