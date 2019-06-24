using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{

    public class SwitchControl
    {
        public SwitchControl(){ }
        //其它业务  
    }


    public class TemControl
    {
        public TemControl() { }
        //其它业务  
    }
    public class LxControl
    {
        public LxControl() { }
        //其它业务  
    }

    public class HWHelper
    {
        private SwitchControl swh;  
    	private TemControl tem;
        private LxControl lx;

        public HWHelper()
        {
            swh = new SwitchControl();
            tem = new TemControl();
            lx = new LxControl();
        }
        //其它业务  
    }

    public class Facade : Command
    {
        public override void Action()
        {
            Console.Clear();
            Console.WriteLine("开始外观模式...");

            HWHelper hw = new HWHelper();

            Console.WriteLine("结束...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
