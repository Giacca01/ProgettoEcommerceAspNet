<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prodotti.aspx.cs" Inherits="ProgettoEcommerce.category" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta
        name="viewport"
        content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link rel="icon" href="img/favicon.png" type="image/png" />
    <title>Giacardi's Shop</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="vendors/linericon/style.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/themify-icons.css" />
    <link rel="stylesheet" href="vendors/owl-carousel/owl.carousel.min.css" />
    <link rel="stylesheet" href="vendors/lightbox/simpleLightbox.css" />
    <link rel="stylesheet" href="vendors/nice-select/css/nice-select.css" />
    <link rel="stylesheet" href="vendors/animate-css/animate.css" />
    <link rel="stylesheet" href="vendors/jquery-ui/jquery-ui.css" />
    <!-- main css -->
    <link rel="stylesheet" href="css/style.css" />
    <link rel="stylesheet" href="css/responsive.css" />
</head>

<body>
    <form id="form1" runat="server">
        <!--================Header Menu Area =================-->
        <header class="header_area">
            <div class="main_menu">
                <div id="contPrincNavBar" class="container" style="max-width:100%">
                    <nav class="navbar navbar-expand-lg navbar-light w-100">
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <a class="navbar-brand logo_h" href="index.html">
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
                                <div id="contNavBar" class="col-lg-10 pr-0" runat="server">
                                    <ul id="navBar" class="nav navbar-nav center_nav pull-right" runat="server">
                                        <li id="navHome" class="nav-item" runat="server">
                                            <a class="nav-link" href="index.aspx">Home</a>
                                        </li>
                                        <li id="navRegistrati" class="nav-item" runat="server">
                                            <a href="registrati.aspx" class="nav-link">Registrati</a>
                                        </li>
                                        <li id="navLogin" class="nav-item" runat="server">
                                            <a href="login.aspx" class="nav-link">Login</a>
                                        </li>
                                        <li id="navReimpostaPwd" class="nav-item" runat="server">
                                            <a href="reimpostaPwd.aspx" class="nav-link">Reimposta Password</a>
                                        </li>
                                        <li id="navProdotti" class="nav-item active" runat="server">
                                            <a href="prodotti.aspx" class="nav-link active">Elenco Prodotti</a>
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
                                        <li id="navGestioneProdotti" class="nav-item" runat="server">
                                            <a href="gestioneProdotti.aspx" class="nav-link">Gestione Prodotti</a>
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
                                <div id="navUtenteCarrrello" class="col-lg-2 pr-0" runat="server">
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
                    <div class="banner_content d-md-flex justify-content-between align-items-center">
                        <div class="mb-3 mb-md-0">
                            <h2>Prodotti</h2>
                        </div>
                        <div class="page_link">
                            <a href="index.aspx">Home</a>
                            <a href="prodotti.aspx">Prodotti</a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--================End Home Banner Area =================-->

        <!--================ Inizio Sezione Prodotti e Ricerche =================-->
        <section class="cat_product_area section_gap">
            <div class="container">
                <div class="row flex-row-reverse">
                    <div class="col-lg-9">
                        <div class="latest_product_inner">
                            <div id="contProdotti" class="row" runat="server">
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-7 col-lg-7 mx-auto">
                                    <div id="msgErroreElProd" role="alert" style="text-align: center;" runat="server"></div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3">
                        <div class="left_sidebar_area">
                            <aside class="left_widgets p_filter_widgets">
                                <div class="l_w_title">
                                    <h3>Nome Prodotto</h3>
                                </div>
                                <div class="widgets_inner">
                                    <div class="input-group mb-3">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="basic-addon1"><i class="fa fa-search" aria-hidden="true"></i></span>
                                        </div>
                                        <input type="text" id="txtNomeProdRic" class="form-control" placeholder="Nome Prodotto" aria-label="Nome Prodotto"
                                            aria-describedby="basic-addon1" runat="server">
                                    </div>
                                </div>
                                <div class="l_w_title">
                                    <h3>Categoria Prodotto</h3>
                                </div>
                                <div class="widgets_inner">
                                    <asp:RadioButtonList ID="elencoCatRic" CssClass="list" runat="server">
                                    </asp:RadioButtonList>
                                </div>
                                <br />
                                <div class="widgets_inner">
                                    <asp:Button ID="btnRicercaProdotto" class="genric-btn primary circle btn-block" runat="server" Text="Prodotto" OnClick="btnRicercaProdotto_Click" />
                                </div>
                            </aside>
                            <div class="row">
                                <div class="col-sm-12 col-md-7 col-lg-7 mx-auto">
                                    <div id="msgRicProd" role="alert" style="text-align: center;" runat="server"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--================ Fine Sezione Prodotti e Ricerche =================-->

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
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/popper.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/stellar.js"></script>
    <script src="vendors/lightbox/simpleLightbox.min.js"></script>
    <script src="vendors/nice-select/js/jquery.nice-select.min.js"></script>
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

