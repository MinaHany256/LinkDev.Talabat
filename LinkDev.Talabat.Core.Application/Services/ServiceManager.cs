using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Services.Products;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;
        private readonly Lazy<IAuthService> _authService;

        public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, Func<IBasketService> basketServiceFactory, Func<IOrderService> orderServiceFactory, Func<IAuthService> authServiceFactory)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
            _orderService = new Lazy<IOrderService>(orderServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _basketService = new Lazy<IBasketService>(basketServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
            _authService = new Lazy<IAuthService>(authServiceFactory, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthService AuthService =>_authService.Value;

        public IOrderService OrderService => _orderService.Value;
    }
}
