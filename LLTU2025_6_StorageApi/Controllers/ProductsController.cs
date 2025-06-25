﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LLTU2025_6_StorageApi.Data;
using LLTU2025_6_StorageApi.Models;
using LLTU2025_6_StorageApi.Models.DTO;

namespace LLTU2025_6_StorageApi.Controllers;

[Route("products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApplicationContext _context;

    public ProductsController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct()
    {
        return await _context.Products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Count = p.Count,
            Category = p.Category,
            Description = p.Description,
            Shelf = p.Shelf,
        }).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Count = product.Count,
            Description = product.Description,
            Category = product.Category,
            Shelf = product.Shelf,
        };
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, UpdateProductDto productDto)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        product.Name = productDto.Name;
        product.Price = productDto.Price;
        product.Count = productDto.Count;
        product.Category = productDto.Category;
        product.Shelf = productDto.Shelf;
        product.Description = productDto.Description;

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> PostProduct(CreateProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            Count = productDto.Count,
            Category = productDto.Category,
            Shelf = productDto.Shelf,
            Description = productDto.Description
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new { id = product.Id }, new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Count = product.Count,
            Description = product.Description,
            Category = product.Category,
            Shelf = product.Shelf,
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.Id == id);
    }
}
