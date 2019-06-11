using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Types;


namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {

        ///////////////////////////////////////////////////////////////////////////////
        //
        // DECLARATIONS
        //
        ///////////////////////////////////////////////////////////////////////////////

        /**
         * Parses the declaration, and constructs an AST to represent its phrase
         * structure.
         * 
         * @return a {@link triangle.compiler.syntax.trees.declarations.Declaration}
         * 
         * @throws SyntaxError
         *           a syntactic error
         * 
         */
        Declaration ParseDeclaration()
        {
            Compiler.WriteDebuggingInfo("Parsing Declaration");
            Location startLocation = tokens.Current.Start;
            Declaration declaration= ParseSingleDeclaration();
            while (tokens.Current.Kind == TokenKind.Semicolon)
            {
                AcceptIt();
                Declaration declaration2 = ParseSingleDeclaration();
                SourcePosition declarationPosition = new SourcePosition(startLocation, tokens.Current.Finish);
                declaration = new SequentialDeclaration(declaration, declaration2, declarationPosition);
            }
            return declaration;

        }

        /**
         * Parses the single declaration, and constructs an AST to represent its
         * phrase structure.
         * 
         * @return a {@link triangle.compiler.syntax.trees.declarations.Declaration}
         * 
         * @throws SyntaxError
         *           a syntactic error
         * 
         */
        Declaration ParseSingleDeclaration()
        {
            Compiler.WriteDebuggingInfo("Parsing Single Declaration");
            Location startLocation = tokens.Current.Start;
            switch (tokens.Current.Kind)
            {

                case TokenKind.Const:
                    {
                        AcceptIt();
                        Identifier identifier = ParseIdentifier();
                        Accept(TokenKind.Is);
                        Expression expression = ParseExpression();
                        SourcePosition  constPosition = new SourcePosition(startLocation, tokens.Current.Finish);
                        return new ConstDeclaration(identifier, expression, constPosition);

                    }

                case TokenKind.Var:
                    {
                        AcceptIt();
                        Identifier identifier = ParseIdentifier();
                        Accept(TokenKind.Colon);
                        TypeDenoter typeDenoter = ParseTypeDenoter();
                        if (tokens.Current.Kind == TokenKind.Becomes)
                        {
                            AcceptIt();
                            Expression expression = ParseExpression();
                            SourcePosition varPosition = new SourcePosition(startLocation, tokens.Current.Finish);
                            return new InitDeclaration (identifier, typeDenoter,expression, varPosition);
                        }
                        else
                        {
                            SourcePosition varPosition = new SourcePosition(startLocation, tokens.Current.Finish);
                            return new VarDeclaration(identifier, typeDenoter, varPosition);
                        }
                    }

                default:
                    {
                        RaiseSyntacticError("\"%\" cannot start a declaration", tokens.Current);
                        return null;
                    }

            }

        }
    }
}