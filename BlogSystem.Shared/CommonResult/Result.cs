using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogSystem.Shared.CommonResult
{
    public class Result
    {
        public bool IsSuccess => _errors.Count == 0;
        
        public bool IsFailed => !IsSuccess;
        private List<Error> _errors = [];


        public IReadOnlyList<Error> Errors
        {
            get { return _errors ; }
        }

        //Constructors
        protected Result()
        {
        }
        protected Result(Error error)
        {
            _errors.Add(error);
        }
        protected Result(List<Error> errors)
        {
            _errors.AddRange(errors);
        }

        //Static Factory Methods
        public static Result Ok() => new Result();
        public static Result Fail(Error error) => new Result(error);
        public static Result Fail(List<Error> errors) => new Result(errors);


    }

    public class Result<TValue>:Result
    {
        private TValue _value;

     

        public TValue Value
        {
            get {
                return IsSuccess ?
                                 _value
                                 :
                                 throw new InvalidOperationException("Can Not Access The Value Of Failed Operation");
            }
        }

        protected Result(TValue value):base()
        {
            _value = value;
        }
        protected Result(Error error):base(error)
        {
            _value = default!;
        }
        protected Result(List<Error> errors):base(errors)
        {
            _value = default!;
        }

        ////
        public static Result<TValue>Ok(TValue value) =>new Result<TValue>(value);
        public new static Result<TValue> Fail(Error error)=>new Result<TValue>(error);
        public new static Result<TValue> Fail(List<Error> errors)=>new Result<TValue>(errors);

        ////Implicit Operator Casting
        public static  implicit operator Result<TValue>(TValue value)=>Ok(value);
        public static  implicit operator Result<TValue>(Error error)=>Fail(error);
        public static  implicit operator Result<TValue>(List<Error> errors)=>Fail(errors);

        

        
    }
}
