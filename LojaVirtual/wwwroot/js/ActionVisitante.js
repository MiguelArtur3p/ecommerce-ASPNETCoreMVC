$(document).ready(function () {
    MoverScrollOrdenacao();
    MudarOrdenacao();
    MudarImagemPrincipalProduto();
});
function MudarImagemPrincipalProduto() {
    $(".img-small-wrap img").click(function(){
        var Caminho = $(this).attr("src");
        $(".img-big-wrap img").attr("src", Caminho);
        $(".img-big-wrap a").attr("href", Caminho);
    });
}
function MoverScrollOrdenacao() {
    if (window.location.hash.length >0) {
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
        var URL=window.location.protocol + "//" + window.location.host + window.location.pathname;
        var URLComParametros = URL + "?pagina=" + Pagina + "&pesquisa=" + Pesquisa + "&ordenacao=" + Ordenacao +Fragmento;
        window.location.href = URLComParametros;
    });
}