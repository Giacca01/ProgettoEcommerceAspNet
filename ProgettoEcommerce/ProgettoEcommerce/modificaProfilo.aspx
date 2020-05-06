﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modificaProfilo.aspx.cs" Inherits="ProgettoEcommerce.ModificaProfilo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script src="js/stellar.js"></script>
    <script src="vendors/lightbox/simpleLightbox.min.js"></script>
    <script src="vendors/isotope/imagesloaded.pkgd.min.js"></script>
    <script src="vendors/isotope/isotope-min.js"></script>
    <script src="vendors/owl-carousel/owl.carousel.min.js"></script>
    <script src="js/jquery.ajaxchimp.min.js"></script>
    <script src="vendors/counter-up/jquery.waypoints.min.js"></script>
    <script src="vendors/counter-up/jquery.counterup.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.12/dist/js/bootstrap-select.min.js"></script>
    <script src="js/mail-script.js"></script>

    <script type="text/javascript">
        //$(document).ready(function () {
        //    refreshListaCitta(-1);
        //});

        function refreshListaCitta(indice) {
            document.getElementById("lstCittaModProfilo").selectedIndex = indice;
            //$('#lstCittaModProfilo').selectpicker('refresh');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!--================Header Menu Area =================-->
        <header class="header_area">
            <div class="main_menu">
                <div class="container">
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
                        <div
                            class="collapse navbar-collapse offset w-100"
                            id="navbarSupportedContent">
                            <div class="row w-100 mr-0">
                                <div class="col-lg-7 pr-0">
                                    <ul class="nav navbar-nav center_nav pull-right">
                                        <li class="nav-item">
                                            <a class="nav-link" href="index.aspx">Home</a>
                                        </li>
                                        <li class="nav-item active submenu dropdown">
                                            <a
                                                href="#"
                                                class="fa fa-sign-out"
                                                data-toggle="dropdown"
                                                role="button"
                                                aria-haspopup="true"
                                                aria-expanded="false">Shop</a>
                                            <ul class="dropdown-menu">
                                                <li class="nav-item">
                                                    <a class="nav-link" href="category.html">Shop Category</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" href="single-product.html">Product Details</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" href="checkout.html">Product Checkout</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" href="cart.html">Shopping Cart</a>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="nav-item submenu dropdown">
                                            <a
                                                href="#"
                                                class="nav-link dropdown-toggle"
                                                data-toggle="dropdown"
                                                role="button"
                                                aria-haspopup="true"
                                                aria-expanded="false">Blog</a>
                                            <ul class="dropdown-menu">
                                                <li class="nav-item">
                                                    <a class="nav-link" href="blog.html">Blog</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" href="single-blog.html">Blog Details</a>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="nav-item submenu dropdown">
                                            <a
                                                href="#"
                                                class="nav-link dropdown-toggle"
                                                data-toggle="dropdown"
                                                role="button"
                                                aria-haspopup="true"
                                                aria-expanded="false">Pages</a>
                                            <ul class="dropdown-menu">
                                                <li class="nav-item">
                                                    <a class="nav-link" href="tracking.html">Tracking</a>
                                                </li>
                                                <li class="nav-item">
                                                    <a class="nav-link" href="elements.html">Elements</a>
                                                </li>
                                            </ul>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" href="contact.html">Contact</a>
                                        </li>
                                    </ul>
                                </div>

                                <div class="col-lg-5 pr-0">
                                    <ul class="nav navbar-nav navbar-right right_nav pull-right">
                                        <li class="nav-item">
                                            <a href="#" class="icons">
                                                <i class="ti-search" aria-hidden="true"></i>
                                            </a>
                                        </li>

                                        <li class="nav-item">
                                            <a href="#" class="icons">
                                                <i class="ti-shopping-cart"></i>
                                            </a>
                                        </li>

                                        <li class="nav-item">
                                            <a href="#" class="icons">
                                                <i class="ti-user" aria-hidden="true"></i>
                                            </a>
                                        </li>

                                        <li class="nav-item">
                                            <a href="#" class="icons">
                                                <i class="ti-heart" aria-hidden="true"></i>
                                            </a>
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
                            <h2>Andamento Vendite</h2>
                        </div>
                        <div class="page_link">
                            <a href="index.aspx">Home</a>
                            <a href="modificaProfilo.aspx" runat="server">Modifica Profilo</a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--================End Home Banner Area =================-->

        <!--================ Sezione Grafici Vendite =================-->
        <section id="sezModificaProfilo" class="cart_area" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12 mx-auto">
                        <div class="main_title">
                            <h2><span>Modifica Profilo</span></h2>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-md-6 col-lg-6 mx-auto" id="contModProfilo" runat="server">
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-user" aria-hidden="true"></i>
                            </div>
                            <input type="text" id="nomeModProfilo" name="nomeModProfilo" placeholder="Nome" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Nome'" required="required" class="single-input" runat="server" />
                        </div>
                        <div class="input-group-icon mt-10" id="contCognomeModProfilo" runat="server">
                            <div class="icon">
                                <i class="fa fa-user-md" aria-hidden="true"></i>
                            </div>
                            <input type="text" id="cognomeModProfilo" name="cognomeModProfilo" placeholder="Cognome" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Cognome'" required="required" class="single-input" runat="server" />
                        </div>
                        <div class="input-group-icon mt-10" id="contDataNascitaModProfilo" runat="server">
                            <div class="icon">
                                <i class="fa fa-calendar" aria-hidden="true"></i>
                            </div>
                            <input type="date" id="dataNascitaModProfilo" name="dataNascitaModProfilo" placeholder="Cognome" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Data Nascita'" required="required" class="single-input" runat="server" />
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-envelope" aria-hidden="true"></i>
                            </div>
                            <input type="email" id="mailModProfilo" name="mailRegModProfilo" placeholder="Mail" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Mail'" required="required" class="single-input" runat="server" />
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-phone-square" aria-hidden="true"></i>
                            </div>
                            <input type="tel" id="telModProfilo" name="telModProfilo" placeholder="Telefono Prefisso Escluso" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Telefono Prefisso Escluso'" required="required" class="single-input" runat="server" />
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-user" aria-hidden="true"></i>
                            </div>
                            <input type="text" id="usernameModProfilo" name="usernameModProfilo" placeholder="Username" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Username'" required="required" class="single-input" runat="server" />
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-lock" aria-hidden="true"></i>
                            </div>
                            <input type="password" id="pwdModProfilo" name="pwdModProfilo" placeholder="Password" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Password'" aria-describedby="descPwdModProfilo" required="required" class="single-input" runat="server" />
                            <small id="descPwdModProfilo" class="form-text text-muted">La Password deve contenere almeno 8 caratteri tra cui almeno una maiuscola, una minuscola, un numero e un carattere tra !@#$%^&*</small>
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-map-marker" aria-hidden="true"></i>
                            </div>
                            <input type="text" id="viaModProfilo" name="viaModProfilo" placeholder="Via" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Via'" required="required" class="single-input" runat="server" />
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-map-marker" aria-hidden="true"></i>
                            </div>
                            <input type="text" id="civicoModProfilo" name="civicoModProfilo" placeholder="Civico" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Civico'" required="required" class="single-input" runat="server" />
                        </div>
                        <div class="input-group-icon mt-10">
                            <div class="icon">
                                <i class="fa fa-building" aria-hidden="true"></i>
                            </div>
                            <div>
                                <select id="lstCittaModProfilo" title="Città Utente" runat="server" class="single-input" required="required">
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-sm-6 col-md-12 col-lg-6 mx-auto">
                        <div id="msgModificaProfilo" role="alert" style="text-align: center;" runat="server"></div>
                    </div>
                </div>
                <div>
                    <div class="col-sm-6 col-md-6 col-lg-6 mx-auto">
                        <div class="form-group">
                            <asp:Button ID="btnModificaProfilo" class="btn btn-success circle btn-block" runat="server" Text="Modifica Profilo" />
                        </div>
                    </div>
                </div>
            </div>
        </section>

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

</body>
</html>