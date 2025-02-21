using System.Security.Cryptography;
using System.Text;

namespace GeradorDeBoletos.Infra.Criptografia;

public class SenhaEncriptador
{
    public string Encriptar(string senha)
    {
        var chaveAdicional = "aquivocepodecolocarqlqrstring";

        var novaSenha = $"{senha}{chaveAdicional}";

        var bytes = Encoding.UTF8.GetBytes(novaSenha);

        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(b);
        }

        return sb.ToString();
    }
}