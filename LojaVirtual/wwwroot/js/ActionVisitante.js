
$(document).ready(function () {
    MoverScrollOrdenacao();
    MudarOrdenacao();
    MudarImagemPrincipalProduto();
    MudarQuantidadeProdutoCarrinho();
    AjaxLocacaoViaCep();
    MascaraCEP();
    AddOrRemoveItem();
    TipoEntrega();
    /* PrecoPacote();*/
});
function MascaraCEP() {
    $("#cepLocation").mask("00.000-000");
}
function numberToReal(numero) {
    var numero = numero.toFixed(2).split('.');
    numero[0] = "R$" + numero[0].split(/(?=(?:...)*$)/).join('.');
    return numero.join(',');
}
function MudarQuantidadeProdutoCarrinho() {
    $("#order .btn-primary").click(function () {
        if ($(this).hasClass("diminuir")) {
            OrquestradorDeAcoesProduto("diminuir", $(this));
        }
        if ($(this).hasClass("aumentar")) {
            OrquestradorDeAcoesProduto("aumentar", $(this));


        }
    });
}
function AddOrRemoveItem() {
    $("#order .btn-primary").click(function () {
        var cepp = $("#buttonCep").parent().parent().find("input[name='cepLocation']").val();
        var quantidade = $(".inputQuantidadeEstoque").val();

        console.log(quantidade);
        if (cepp != null & cepp != "") {
            if ($("#SEDEX").is(":checked")) {
                respostaTipoFrete = "S";
                PrecoPacote(respostaTipoFrete);
            } else if ($("#COMUM").is(":checked")) {
                respostaTipoFrete = "C";
                PrecoPacote(respostaTipoFrete);
            }

        } else {
            $("#freteTotal").text("R$ 0,00");
            $("#valorTotal").text("R$ 0,00");

        }
    });
}
function TipoEntrega() {
    var respostaTipoFrete;
    $("#SEDEX").click(function () {
        respostaTipoFrete = "S";
        $.cookie("Carrinho.TipoFrete", respostaTipoFrete);
        PrecoPacote(respostaTipoFrete);
    });
    $("#COMUM").click(function () {
        respostaTipoFrete = "C";
        $.cookie("Carrinho.TipoFrete", respostaTipoFrete);
        PrecoPacote(respostaTipoFrete);
    });
}
function PrecoPacote(respostaTipoFrete) {
    var subtotal = $(".price");
    var subTotalLimpo = parseFloat(subtotal.text().replace("R$", "").replace(".", "").replace(" ", "").replace(",", "."));
    var quantidade = $(".inputQuantidadeEstoque").val();
    fetch("https://localhost:44318/api/PrecoPacotes/tipoEnvio?tipoFrete=" + respostaTipoFrete)
        .then(function (response) {
            if (!response.ok) throw new Error("Erro ao executar a requisição");
            return response.text();
        })
        .then(function (data) {
            var Total = subTotalLimpo + parseFloat(data);
            $("#freteTotal").text("R$ " + data.split(".").join(","));
            if (quantidade < 1) {
                $("#valorTotal").text("R$ 0,00");
            }
            $("#valorTotal").text(numberToReal(Total));


        })
        .catch(error => {
            console.error(error.message);

        });
}
function AjaxLocacaoViaCep() {
    $("#buttonCep").click(function () {
        var cep = $(this).parent().parent().find("input[name='cepLocation']").val().replace(".", "").replace("-", "");

        var url = "https://viacep.com.br/ws/" + cep + "/json/";

        $.removeCookie("Carrinho.TipoFrete");

        fetch(url)
            .then(function (response) {
                if (!response.ok) throw new Error("Erro ao executar a requisição");
                return response.json();
            })
            .then(function (data) {
                if (data.erro != true) {
                    $("#nameCity").text(data.localidade + "-" + data.uf);
                    $("#HiddenOnOrOf").removeClass("HiddenOnOrOf");
                } else {
                    $("#nameCity").text("Cep inválido.");
                }

            })
            .catch(error => {
                console.error("Erro na requisiçao ", error.message);
                $("#nameCity").text("Cep inválido.");
            });



    });
}


function OrquestradorDeAcoesProduto(operacao, botao) {
    OcultarMensagemDeErro();
    var pai = botao.parent().parent();
    var produtoId = pai.find(".inputProdutoId").val();
    var quantidadeEstoque = parseInt(pai.find(".inputQuantidadeEstoque").val());
    var valorUnitario = parseFloat(pai.find(".inputValorUnitario").val().replace(",", "."));
    var campoQuantidadeProdutoCarrinho = pai.find(".inputQuantidadeCarrinho");
    var quantidadeProdutoCarrinhoAntiga = parseInt(campoQuantidadeProdutoCarrinho.val());
    var campoValor = botao.parent().parent().parent().parent().parent().find(".price");
    var produto = new ProdutoQuantidadeEValor(produtoId, quantidadeEstoque, valorUnitario, quantidadeProdutoCarrinhoAntiga, 0, campoQuantidadeProdutoCarrinho, campoValor);

    AlteracoesVisuaisProdutoCarrinho(produto, operacao);
}
function AlteracoesVisuaisProdutoCarrinho(produto, operacao) {
    if (operacao == "aumentar") {
        /* if (produto.quantidadeProdutoCarrinhoAntiga== produto.quantidadeEstoque) {
             alert("Quantidade indisponivel no estoque!");
         } else */
        produto.quantidadeProdutoCarrinhoNova = produto.quantidadeProdutoCarrinhoAntiga + 1;
        AtualizarQuantidadeEValor(produto);
        AJAXComunicarAlteracaoQuantidadeProduto(produto);
    }
    else if (operacao == "diminuir") {
        /* if (produto.quantidadeProdutoCarrinhoAntiga == 1) {
             alert("Caso não deseje este produto clique em Remover!")
         } else*/
        produto.quantidadeProdutoCarrinhoNova = produto.quantidadeProdutoCarrinhoAntiga - 1;
        if (produto.quantidadeProdutoCarrinhoAntiga > 1) {
            AtualizarQuantidadeEValor(produto);
            AJAXComunicarAlteracaoQuantidadeProduto(produto);
        } else {
            alert("Quantidade do produto não pode ser menor que 0, caso não queira o produto, clique em remover!")
        }

    }
}

function AtualizarQuantidadeEValor(produto) {
    produto.campoQuantidadeProdutoCarrinho.val(produto.quantidadeProdutoCarrinhoNova);

    var resultado = produto.valorUnitario * produto.quantidadeProdutoCarrinhoNova;
    produto.campoValor.text(numberToReal(resultado));
    AtualizarSubtotal();
}

function AtualizarSubtotal() {
    var Subtotal = 0;

    var TagsComPrice = $(".price");

    TagsComPrice.each(function () {
        var ValorReais = parseFloat($(this).text().replace("R$", "").replace(".", "").replace(" ", "").replace(",", "."));
        Subtotal += ValorReais;
    })
    $("#subTotal").text(numberToReal(Subtotal));


}

function AJAXComunicarAlteracaoQuantidadeProduto(produto) {
    $.ajax({
        type: "GET",
        url: "/CarrinhoCompra/AlterarQuantidade?id=" + produto.produtoId + "&quantidade=" + produto.quantidadeProdutoCarrinhoNova,
        success: function () {


        },
        error: function (data) {
            MostrarMensagemDeErro(data.responseJSON.mensagem);

            produto.quantidadeProdutoCarrinhoNova = produto.quantidadeProdutoCarrinhoAntiga;
            AtualizarQuantidadeEValor(produto);
        },
    })
}
function MostrarMensagemDeErro(mensagem) {
    $(".alert-danger").css("display", "block");
    $(".alert-danger").text(mensagem);
}
function OcultarMensagemDeErro() {
    $(".alert-danger").css("display", "none");

}
function MudarImagemPrincipalProduto() {
    $(".img-small-wrap img").click(function () {
        var Caminho = $(this).attr("src");
        $(".img-big-wrap img").attr("src", Caminho);
        $(".img-big-wrap a").attr("href", Caminho);
    });
}
function MoverScrollOrdenacao() {
    if (window.location.hash.length > 0) {
        var hash = window.location.hash;
        if (hash == "#posicao-produto") {
            window.scrollBy(0, 473);
        }
    }
}
function MudarOrdenacao() {
    $("#ordenacao").change(function () {
        //TODO - REDIRECT TO HOME/INDEX send a querystring ordeby and keeping a page and shearch
        var Pagina = 1;
        var Pesquisa = "";
        var Ordenacao = $(this).val();
        var Fragmento = "#posicao-produto";

        var QueryString = new URLSearchParams(window.location.search);
        if (QueryString.has("pagina")) {
            Pagina = QueryString.get("pagina");
        }
        if (QueryString.has("pesquisa")) {
            Pesquisa = QueryString.get("pesquisa");
        }
        if ($("#breadcrumb").length > 0) {
            Fragmento = "";
        }
        var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;
        var URLComParametros = URL + "?pagina=" + Pagina + "&pesquisa=" + Pesquisa + "&ordenacao=" + Ordenacao + Fragmento;
        window.location.href = URLComParametros;
    });
}

/*CLASSES*/
class ProdutoQuantidadeEValor {
    constructor(produtoId, quantidadeEstoque, valorUnitario, quantidadeProdutoCarrinhoAntiga, quantidadeProdutoCarrinhoNova, campoQuantidadeProdutoCarrinho, campoValor) {
        this.produtoId = produtoId;
        this.quantidadeEstoque = quantidadeEstoque;
        this.valorUnitario = valorUnitario;

        this.quantidadeProdutoCarrinhoAntiga = quantidadeProdutoCarrinhoAntiga;
        this.quantidadeProdutoCarrinhoNova = quantidadeProdutoCarrinhoNova;

        this.campoQuantidadeProdutoCarrinho = campoQuantidadeProdutoCarrinho;
        this.campoValor = campoValor;
    }
}