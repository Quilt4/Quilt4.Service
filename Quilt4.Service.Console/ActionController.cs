//using System;
//using System.Web.Http;
//using Quilt4.Service.Business.Command;

//namespace Quilt4.Service.Console
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
//        public object Get(string handler, string data)
//        {
//            //TODO: Log
//            //TODO: Invoke a query handler for the provided object.
//            throw new NotImplementedException();
//            return "Hello, World!";
//        }

//        // POST api/demo 
//        public void Post(string handler, [FromBody]object data)
//        {
//            //TODO: Register to be replayed later
//            _commandQueue.Enqueue(new Tuple<string, object>(handler, data));
//            //TODO: Invoke a query handler for the provided object.
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