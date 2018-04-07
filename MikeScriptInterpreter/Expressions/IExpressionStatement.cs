namespace MikeScriptInterpreter
{

    public abstract class ExpressionStatement : IStatement
    {
        public void Execute()
        {
            //do nothing
        }

        public abstract object Value();
    };
}