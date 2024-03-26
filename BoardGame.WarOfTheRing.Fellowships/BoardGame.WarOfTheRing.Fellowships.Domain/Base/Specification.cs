using System.Linq.Expressions;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Base;

public abstract class Specification<T>
{
    public static readonly Specification<T> All = new IdentitySpecification<T>();
    public bool IsSatisfiedBy(T entity) => ToPredicate()(entity);

    public abstract Expression<Func<T, bool>> ToExpression();
    public Func<T, bool> ToPredicate() => ToExpression().Compile();

    public Specification<T> And(Specification<T> specification)
    {
        if(this == All)
        {
            return specification;
        }

        if(specification == All)
        {
            return this;
        }

        return new AndSpecification<T>(this, specification);
    }

    public Specification<T> Or(Specification<T> specification)
    {
        if(this == All || specification == All)
        {
            return All;
        }

        return new OrSpecification<T>(this, specification);
    }

    public Specification<T> Not() => new NotSpecification<T>(this);
}

internal sealed class AndSpecification<T>(Specification<T> left, Specification<T> right) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = left.ToExpression();
        var rightExpression = right.ToExpression();

        var parameterExpression = leftExpression.Parameters.Single();
        var andExpression = new ParameterReplacer(parameterExpression).Visit(Expression.AndAlso(leftExpression.Body, rightExpression.Body));

        return Expression.Lambda<Func<T, bool>>(andExpression, parameterExpression);
    }
}

internal sealed class OrSpecification<T>(Specification<T> left, Specification<T> right) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var leftExpression = left.ToExpression();
        var rightExpression = right.ToExpression();

        var parameterExpression = leftExpression.Parameters.Single();
        var orExpression = new ParameterReplacer(parameterExpression).Visit(Expression.OrElse(leftExpression.Body, rightExpression.Body));

        return Expression.Lambda<Func<T, bool>>(orExpression, parameterExpression);
    }
}

internal sealed class NotSpecification<T>(Specification<T> specification) : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var specificationExpression = specification.ToExpression();

        var parameterExpression = specificationExpression.Parameters.Single();
        var notExpression = new ParameterReplacer(parameterExpression).Visit(Expression.Not(specificationExpression.Body));

        return Expression.Lambda<Func<T, bool>>(notExpression, parameterExpression);
    }
}

internal sealed class IdentitySpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression() => _ => true;
}

internal class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression parameter;

    protected override Expression VisitParameter(ParameterExpression node) => base.VisitParameter(parameter);

    internal ParameterReplacer(ParameterExpression parameter)
    {
        this.parameter = parameter;
    }
}