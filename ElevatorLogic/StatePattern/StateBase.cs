using log4net;

namespace ElevatorLogic.StatePattern
{
    internal abstract class StateBase : IElevatorStateAction
    {
        private static readonly ILog m_Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initializes a new instance of the <see cref="StateBase"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public StateBase(StateContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets or sets the next action.
        /// </summary>
        /// <value>
        /// The next action.
        /// </value>
        public IElevatorStateAction NextAction { get; protected set; } = null;

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected StateContext Context { get; private set; }

        /// <summary>
        /// Executes the state logic.
        /// </summary>
        public abstract void ExecuteStateLogic();
    }
}