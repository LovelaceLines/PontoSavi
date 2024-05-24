using FluentValidation;

namespace PontoSavi.Application.Validators;

public class GlobalValidator
{
    public static string RequiredField(string field) => $"Campo {field} obrigatório.";
    public static string InvalidField(string field) => $"Campo {field} inválido.";
    public static string MaxLength(string field, int length) => $"Campo {field}. Tamanho/Valor máximo excedido. Máximo de {length} caracteres.";
    public static string MinLength(string field, int length) => $"Campo {field}. Tamanho/Valor mínimo não atingido. Mínimo de {length} caracteres.";
    public static string InvalidEmailFormat() => "E-mail inválido.";
    public static string InvalidDateTimeFormat() => "Data inválida. Use o formato dd/MM/yyyy HH:mm";
    public static string InvalidDateFormat() => "Data inválida. Use o formato dd/MM/yyyy";
}