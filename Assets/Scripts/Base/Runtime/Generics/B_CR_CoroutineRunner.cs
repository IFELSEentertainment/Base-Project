using System.Threading.Tasks;
namespace Base {
    public class B_CR_CoroutineRunner : B_ManagerBase {
        public static B_CR_CoroutineRunner instance;
        public B_CoroutineQueue CQ;

        public override Task ManagerStrapping() {
            if (instance == null) instance = this;
            else Destroy(gameObject);
            CQ = new B_CoroutineQueue(this);
            CQ.StartLoop();
            return base.ManagerStrapping();
        }

        public override Task ManagerDataFlush() {
            instance = null;
            return base.ManagerDataFlush();
        }
    }
}