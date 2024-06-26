﻿using Domain.Entities;
using System.Linq.Expressions;

namespace Application;

public interface IMongoRepository<TDocument> where TDocument : IDocument
{
    IQueryable<TDocument> AsQueryable();
    TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

    Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);


    Task InsertOneAsync(TDocument document);


    Task ReplaceOneAsync(TDocument document);


    Task DeleteOneAsync(Expression<Func<TDocument, bool>> filterExpression);

}