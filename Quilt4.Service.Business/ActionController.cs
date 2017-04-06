//using System;
//using System.Web.Http;
//using Quilt4.Service.Business.Command;

//namespace Quilt4.Service.Business
//{
//    public class ActionController : ApiController
//    {
//        private readonly ICommandQueue _commandQueue;

//        public ActionController(ICommandQueue commandQueue)
//        {
//            _commandQueue = commandQueue;
//        }

//        // GET api/demo 
//        public object Get()
//        {
//            throw new NotSupportedException();
//        }

//        // GET api/demo/5 
//        public object Get(string handler, string data = null)
//        {
//            //TODO: Serialize data to a query of type IQuery.
//            //Execute and return response.
//            //The response should be returned as a SignalR stream. (Same as an event)
//            //throw new NotImplementedException();
//            Console.WriteLine("GET " + handler);

//            return null;
//        }

//        // POST api/demo 
//        public void Post(string handler, [FromBody]object data)
//        {
//            Console.WriteLine("POST " + handler);

//            //TODO: Serialize data to a command of type ICommand.
//            var command = new SomeCommand();
//            _commandQueue.Enqueue(command);
//        }

//        // PUT api/demo/5 
//        public void Put(string handler, int id, [FromBody]string data)
//        {
//            //TODO: Invoke a query handler for the provided object.
//            throw new NotImplementedException();
//        }

//        // DELETE api/demo/5 
//        public void Delete(string handler, string data)
//        {
//            //TODO: Invoke a query handler for the provided object.
//            throw new NotImplementedException();
//        }
//    }
//}
