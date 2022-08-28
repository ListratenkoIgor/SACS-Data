using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Models;
using Interfaces.DTOs;
using DataService.Data;
using AutoMapper;

namespace DataService.Controllers
{
    public abstract class MyControllerBase : ControllerBase
    {
        protected UnitOfWork _unitOfWork;
        protected IMapper _mapper;
        public MyControllerBase(UnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
