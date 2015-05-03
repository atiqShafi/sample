using System.Linq.Expressions;
using System.Text;

namespace Sample.Web.Mvc.Kendo
{
    public static class LambdaExpressionExtensions
    {
        private class PropertyNameComposer : ExpressionVisitor
        {
            private readonly StringBuilder _propertyNamebuilder;

            public string PropertyName
            {
                get { return _propertyNamebuilder.ToString(); }
            }

            public PropertyNameComposer()
            {
                _propertyNamebuilder = new StringBuilder();
            }


            protected override Expression VisitMember( MemberExpression node )
            {
                var result = base.VisitMember( node );
                if ( _propertyNamebuilder.Length != 0 )
                {
                    _propertyNamebuilder.Append( '.' );
                }
                _propertyNamebuilder.Append( node.Member.Name );
                return result;
            }
        }

        /// <summary>
        /// Pomocná funkce, která sestaví stringový zápis vlastnoti, na kterou se odkazujeme lambda výrazem.
        /// Pozor: předpokládá správnou formulaci výrazu e => MemberExpression(e) (i zanořeně). Nelze takto zpracovat identitu.
        /// Ostatní uzly ve stromu výrazu se ignorují (tedy např. případný Convert).
        /// </summary>
        /// <param name="expression">Lambda výraz vybýrající vlastnost entity ve formátu Func[Entity,?]</param>
        public static string GetPropertyName( this LambdaExpression expression )
        {
            var visitor = new PropertyNameComposer();
            visitor.Visit( expression );
            return visitor.PropertyName;
        }
    }
}