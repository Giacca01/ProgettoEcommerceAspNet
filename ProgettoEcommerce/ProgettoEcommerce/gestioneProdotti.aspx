﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gestioneProdotti.aspx.cs" Inherits="ProgettoEcommerce.gestioneProdotti" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta
        name="viewport"
        content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link rel="icon" href="img/favicon.png" type="image/png" />
    <title>Eiser ecommerce</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="vendors/linericon/style.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/themify-icons.css" />
    <link rel="stylesheet" href="vendors/owl-carousel/owl.carousel.min.css" />
    <link rel="stylesheet" href="vendors/lightbox/simpleLightbox.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.12/dist/css/bootstrap-select.min.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.12/dist/css/bootstrap-select.min.css" />
    <link rel="stylesheet" href="vendors/animate-css/animate.css" />
    <link rel="stylesheet" href="vendors/jquery-ui/jquery-ui.css" />
    <!-- main css -->
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/responsive.css" />
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/popper.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.12/dist/js/bootstrap-select.min.js"></script>
    <script type="text/javascript">
        function refreshLista(idLista, indice) {
            if (indice != -2) {
                document.getElementById(idLista).selectedIndex = indice;
            }

            $('#' + idLista).selectpicker('refresh');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!--================Header Menu Area =================-->
        <header class="header_area">
            <div class="main_menu">
                <div class="container" style="max-width:100%">
                    <nav class="navbar navbar-expand-lg navbar-light w-100">
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <a class="navbar-brand logo_h" href="index.aspx">
                            <img src="img/logo.png" alt="" />
                        </a>
                        <button
                            class="navbar-toggler"
                            type="button"
                            data-toggle="collapse"
                            data-target="#navbarSupportedContent"
                            aria-controls="navbarSupportedContent"
                            aria-expanded="false"
                            aria-label="Toggle navigation">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <!-- Collect the nav links, forms, and other content for toggling -->
                        <div class="collapse navbar-collapse offset w-100" id="navbarSupportedContent">
                            <div class="row w-100 mr-0">
                                <div id="contNavBar" class="col-lg-7 pr-0" runat="server">
                                    <ul id="navBar" class="nav navbar-nav center_nav pull-right" runat="server">
                                        <li id="navProdotti" class="nav-item" runat="server">
                                            <a href="prodotti.aspx" class="nav-link">Elenco Prodotti</a>
                                        </li>
                                        <li id="navAndamentoVendite" class="nav-item" runat="server">
                                            <a href="andamentoVendite.aspx" class="nav-link">Andamento Vendite</a>
                                        </li>
                                        <li id="navCarrello" class="nav-item" runat="server">
                                            <a href="carrello.aspx" class="nav-link">Carrello</a>
                                        </li>
                                        <li id="navCategorie" class="nav-item" runat="server">
                                            <a href="categorie.aspx" class="nav-link">Categorie</a>
                                        </li>
                                        <li id="navGestioneOrdini" class="nav-item" runat="server">
                                            <a href="gestioneOrdini.aspx" class="nav-link">Gestione Ordini</a>
                                        </li>
                                        <li id="navGestioneProdotti" class="nav-item active" runat="server">
                                            <a href="gestioneProdotti.aspx" class="nav-link active">Gestione Prodotti</a>
                                        </li>
                                        <li id="navGestioneUtenti" class="nav-item" runat="server">
                                            <a href="gestioneUtenti.aspx" class="nav-link">Gestione Utenti</a>
                                        </li>
                                        <li id="navStoricoOrdini" class="nav-item" runat="server">
                                            <a href="storicoOrdini.aspx" class="nav-link">Storico Ordini</a>
                                        </li>
                                        <li id="navTipiCarte" class="nav-item" runat="server">
                                            <a href="TipiCarteCredito.aspx" class="nav-link">Carte di Credito</a>
                                        </li>
                                    </ul>
                                </div>
                                <div id="navUtenteCarrrello" class="col-lg-5 pr-0" runat="server">
                                    <ul class="nav navbar-nav navbar-right right_nav pull-right">
                                        <li class='nav-item submenu dropdown'>
                                            <a href='#' class='icons dropdown-toggle' data-toggle='dropdown' role="button" aria-haspopup='true'><i class='ti-user' aria-hidden='true'></i></a>
                                            <ul id="dropDownLogout" class='dropdown-menu' runat="server">
                                                <li id="contLogout" class='nav-item' runat="server"></li>
                                                <li class='nav-item'>
                                                    <a href="modificaProfilo.aspx" class='icons'><i class="ti-user" aria-hidden='true'></i>Profilo</a>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class='nav-item'>
                                            <a href="carrello.aspx" class='icons'><i class='ti-shopping-cart' aria-hidden='true'></i></a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </nav>
                </div>
            </div>
        </header>
        <!--================Header Menu Area =================-->

        <!--================Home Banner Area =================-->
        <section class="banner_area">
            <div class="banner_inner d-flex align-items-center">
                <div class="container">
                    <div
                        class="banner_content d-md-flex justify-content-between align-items-center">
                        <div class="mb-3 mb-md-0">
                            <h2>Gestione Prodotti</h2>
                        </div>
                        <div class="page_link">
                            <a href="index.aspx">Home</a>
                            <a href="gestioneProdotti.aspx" runat="server">Gestione Prodotti</a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--================End Home Banner Area =================-->

        <!--================ Sezione Elenco Prodotti =================-->
        <section id="sezElencoProdotti" class="cart_area" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-sm-12 col-md-12 col-lg-12 col-xs-12 mx-auto table-responsive">
                        <table id="tabellaElencoProdotti" class="table table-striped table-hover table-bordered" style="text-align: center;">
                            <thead class="thead-inverse">
                                <tr id="rigaTabElencoCategorie">
                                    <th>Modello</th>
                                    <th>Marca</th>
                                    <th>Categoria</th>
                                    <th>Fornitore</th>
                                    <th>Prezzo</th>
                                    <th>Sconto</th>
                                    <th>Giacenza</th>
                                    <th>Valido</th>
                                    <th>Azioni</th>
                                    <%--Modifica/Elimina--%>
                                </tr>
                            </thead>
                            <tbody id="corpoTabElencoProdotti" runat="server"></tbody>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12 col-md-7 col-lg-7 mx-auto">
                        <div id="msgElencoProdotti" role="alert" style="text-align: center;" runat="server"></div>
                    </div>
                </div>
            </div>
        </section>
        <!--================ Fine Sezione Elenco Prodotti =================-->

        <!--================ Sezione Inserimento/Modifica Prodotti =================-->
        <section id="sezInsModProdotti" class="cart_area" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 mx-auto">
                        <div class="main_title">
                            <h2><span id="titoloSezInsModProdotti" runat="server">Inserimento Prodotti</span></h2>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-4 col-lg-4 mx-auto">
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-user" aria-hidden="true"></i>
                            </div>
                            <input type="text" id="modelloInsModProdotti" name="modelloInsModProdotti" placeholder="Modello"
                                onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Modello'"
                                class="single-input" runat="server" required="required" />
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-user" aria-hidden="true"></i>
                            </div>
                            <textarea id="descrizioneInsModProdotti" class="single-textarea single-input" placeholder="Descrizione" onfocus="this.placeholder = ''" onblur="this.placeholder = 'Descrizione'" required="required" runat="server"></textarea>
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-user" aria-hidden="true"></i>
                            </div>
                            <input type="text" id="marcaInsModProdotti" name="marcaInsModProdotti" placeholder="Marca"
                                onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Marca'"
                                class="single-input" runat="server" required="required" />
                        </div>
                        <div id="contLstGestImg" class="input-group-icon mt-10" runat="server">
                            <div class="icon">
                                <i class="fa fa-building" aria-hidden="true"></i>
                            </div>
                            <div>
                                <select id="lstGestImg" title="Immagine Prodotto" runat="server" class="single-input form-control" required="required" onserverchange="lstGestImg_ServerChange">
                                    <option value="attuale" selected="selected">Mantieni Attuale</option>
                                    <option value="nuova">Inserisci Nuova</option>
                                </select>
                            </div>
                        </div>
                        <div id="contImgProd" class="input-group-icon mt-10" runat="server">
                            <div class="input-group-prepend">
                                <span class="input-group-text">
                                    <i class="fa fa-paperclip"></i>
                                </span>
                                <div class="custom-file">
                                    <input type="file" id="immagineInsModProdotti" class="custom-file-input single-input" name="immagineInsModProdotti" accept="image/*" required="required" runat="server" />
                                    <label class="custom-file-label" for="immagineInsModProdotti">Scegli Immagine</label>
                                </div>
                            </div>
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-building" aria-hidden="true"></i>
                            </div>
                            <div>
                                <select id="lstCatInsModProdotti" title="Categoria Prodotto" runat="server" class="single-input form-control" required="required">
                                </select>
                            </div>
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-user" aria-hidden="true"></i>
                            </div>
                            <input type="number" id="prezzoInsModProdotti" name="prezzoInsModProdotti" placeholder="Prezzo"
                                onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Prezzo'"
                                class="single-input" runat="server" required="required" />
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-user" aria-hidden="true"></i>
                            </div>
                            <input type="number" id="scontoInsModProdotti" name="scontoInsModProdotti" placeholder="Sconto"
                                onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Sconto'"
                                class="single-input" aria-describedby="descSconto" runat="server" required="required" />
                            <small id="descSconto" class="form-text text-muted">In Percentuale
                            </small>
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-user" aria-hidden="true"></i>
                            </div>
                            <input type="number" id="giacenzaInsModProdotti" name="giacenzaInsModProdotti" placeholder="Giacenza"
                                onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Giacenza'"
                                class="single-input" runat="server" required="required" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12 col-md-4 col-lg-4 mx-auto">
                        <div id="msgInsModProdotti" role="alert" style="text-align: center;" runat="server"></div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-12 col-md-4 col-lg-4 mx-auto">
                        <div class="form-group">
                            <asp:LinkButton ID="btnInsModProdotti" CssClass="genric-btn primary circle btn-block" runat="server" OnClick="btnInsModProdotti_Click"><i class="fa fa-plus" aria-hidden="true"></i> Aggiungi</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--================ Fine Sezione Inserimento/Modifica Prodotti =================-->

        <!--================ start footer Area  =================-->
        <footer class="footer-area section_gap">
            <div class="container">
                <div class="row">
                    <div class="col-lg-4 col-md-6 single-footer-widget">
                        <h4>Giacardi's Store</h4>
                        <p>Un negozio all'avanguardia per tutte le tue esigenze</p>
                    </div>
                    <div class="col-lg-4 col-md-6 single-footer-widget"></div>
                    <div class="col-lg-4 col-md-6 single-footer-widget">
                        <h4>Contatti</h4>
                        <ul>
                            <li>+3984190593</li>
                            <li>Via Genova, 105 Palermo</li>
                            <li>info@giacardistore.com</li>
                        </ul>
                    </div>
                </div>
                <div class="footer-bottom row align-items-center">
                    <p class="footer-text m-0 col-lg-8 col-md-12">
                        <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
                        Copyright &copy;<script>document.write(new Date().getFullYear());</script>
                        All rights reserved | This template is made with <i class="fa fa-heart-o" aria-hidden="true"></i>by <a href="https://colorlib.com" target="_blank">Colorlib</a>
                        <!-- Link back to Colorlib can't be removed. Template is licensed under CC BY 3.0. -->
                    </p>
                    <div class="col-lg-4 col-md-12 footer-social">
                        <a href="#"><i class="fa fa-facebook"></i></a>
                        <a href="#"><i class="fa fa-twitter"></i></a>
                        <a href="#"><i class="fa fa-dribbble"></i></a>
                        <a href="#"><i class="fa fa-behance"></i></a>
                    </div>
                </div>
            </div>
        </footer>
        <!--================ End footer Area  =================-->
    </form>
    <!-- Optional JavaScript -->
    <!-- jQuery first, then Popper.js, then Bootstrap JS -->
    <script src="js/stellar.js"></script>
    <script src="vendors/lightbox/simpleLightbox.min.js"></script>
    <script src="vendors/isotope/imagesloaded.pkgd.min.js"></script>
    <script src="vendors/isotope/isotope-min.js"></script>
    <script src="vendors/owl-carousel/owl.carousel.min.js"></script>
    <script src="js/jquery.ajaxchimp.min.js"></script>
    <script src="js/mail-script.js"></script>
    <script src="vendors/jquery-ui/jquery-ui.js"></script>
    <script src="vendors/counter-up/jquery.waypoints.min.js"></script>
    <script src="vendors/counter-up/jquery.counterup.js"></script>
    <script src="js/theme.js"></script>
</body>
</html>
