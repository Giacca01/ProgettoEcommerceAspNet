$(document).ready(function () {
    document.getElementById("lstCartePagamento").selectedIndex = -1;
    $('#lstCartePagamento').selectpicker('refresh');
    $("#btnGestOrdine").click(apriModal);
});

function apriModal() {
    if (document.getElementById("lstCartePagamento").selectedIndex != 0) {
        $("#msgErrCartaCredito").html("");
        $("#msgErroreConfermaOrdine").html("");
        $("#msgConfermaOrdine").text("Sei sicuro di voler ordinare i prodotti nel carrello ?");
        $("#modalConfermaOrdine").modal('show');
    } else {
        $("#msgErrCartaCredito").html("Indicare una carta di credito");
    }
    
}