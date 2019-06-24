using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern
{
    public abstract class Command
    {

        public Command()
        {
        }

        public abstract void Action();
    }

    public class Invoker
    {
        public Command _command;

        public Invoker(Command command)
        {
            this._command = command;
        }

        public void ExecuteCommand()
        {
            _command.Action();
        }
    }
}
