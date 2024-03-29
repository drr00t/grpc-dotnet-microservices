// Copyright (c) 2022 Adriano Ribeiro
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
// of the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
// OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Grpc.Core;
using Microsoft.Extensions.Logging;
using product.api;

namespace GrpcServerSvc.Services;

public class ProductServiceV1: ProductService.ProductServiceBase
{
    private readonly ILogger _logger;
    private readonly Random _random = new Random();
    private readonly IList<string> products = new List<string>(){"Apple", "Samsung", "Nokia", "Pixel", "LG"};
    
    public ProductServiceV1(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ProductServiceV1>();
        
        
    }
    
    public override Task<GetProductResponse> getProduct(GetProductRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"Get Product number {request.ProductId}");
        var headers = context.RequestHeaders.Count;
        
        return Task.FromResult(new GetProductResponse
        {
            Description = $"description {request.ProductId}", 
            Name = $"{_random.Next(products.Count)}", 
            Price = 2f * _random.NextSingle() * 100
        });
    }
}