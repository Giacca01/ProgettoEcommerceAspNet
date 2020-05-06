﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registrati.aspx.cs" Inherits="ProgettoEcommerce.registrati" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <link rel="icon" href="img/favicon.png" type="image/png" />
    <title>Giacardi's Shop</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/bootstrap.css" />
    <link rel="stylesheet" href="vendors/linericon/style.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <link rel="stylesheet" href="css/themify-icons.css" />
    <link rel="stylesheet" href="css/flaticon.css" />
    <link rel="stylesheet" href="vendors/owl-carousel/owl.carousel.min.css" />
    <link rel="stylesheet" href="vendors/lightbox/simpleLightbox.css" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.12/dist/css/bootstrap-select.min.css">
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
                <div class="container">
                    <nav class="navbar navbar-expand-lg navbar-light w-100">
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <a class="navbar-brand logo_h" href="index.html">
                            <img src="img/logo.png" alt="" />
                        </a>
                        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent"
                            aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <!-- Collect the nav links, forms, and other content for toggling -->
                        <div class="collapse navbar-collapse offset w-100" id="navbarSupportedContent">
                            <div class="row w-100 mr-0">
                                <div class="col-lg-12 pr-0">
                                    <ul class="nav navbar-nav center_nav pull-right">
                                        <li class="nav-item">
                                            <a class="nav-link" href="index.html">Home</a>
                                        </li>
                                        <li class="nav-item">
                                            <a href="prodotti.aspx" class="nav-link" role="button"
                                                aria-expanded="false">Elenco Prodotti</a>
                                        </li>
                                        <li class="nav-item">
                                            <a href="registrati.aspx" class="nav-link active" role="button"
                                                aria-expanded="false">Registrati</a>
                                        </li>
                                        <li class="nav-item">
                                            <a href="login.aspx" class="nav-link" role="button"
                                                aria-expanded="false">Login</a>
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

        <!--================ Home Banner Area =================-->
        <section class="section_gap">
            <div class="banner_inner d-flex align-items-center">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8 mx-auto">
                            <div class="main_title">
                                <h2><span>Registrati</span></h2>
                            </div>
                            <div class="col-sm-6 col-md-6 col-lg-6 mx-auto">
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-building" aria-hidden="true"></i>
                                    </div>
                                    <div>
                                        <asp:DropDownList id="lstTipoUtenteReg" title="Tipo Utente" runat="server" class="single-input" required="required" AutoPostBack="true" OnSelectedIndexChanged="lstTipoUtenteReg_SelectedIndexChanged">
                                            <asp:ListItem Value="cliente">Cliente</asp:ListItem>
                                            <asp:ListItem Value="admin">Admin</asp:ListItem>
                                            <asp:ListItem Value="fornitore">Fornitore</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-user" aria-hidden="true"></i>
                                    </div>
                                    <input type="text" id="nomeReg" name="nomeReg" placeholder="Nome" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Nome'" required class="single-input" runat="server">
                                </div>
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-user-md" aria-hidden="true"></i>
                                    </div>
                                    <input type="text" id="cognomeReg" name="cognomeReg" placeholder="Cognome" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Cognome'" required class="single-input" runat="server">
                                </div>
                                <div class="input-group-icon mt-10" id="contDataNascitaReg" runat="server">
                                    <div class="icon">
                                        <i class="fa fa-calendar" aria-hidden="true"></i>
                                    </div>
                                    <input type="date" id="dataNascitaReg" name="dataNascitaReg" placeholder="Data Nascita" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Data Nascita'" aria-describedby="descDataNascitaReg" required class="single-input" runat="server">
                                    <small id="descDataNascitaReg" class="form-text text-muted">Devi avere almeno 18 anni per iscriverti
                                    </small>
                                </div>
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-envelope" aria-hidden="true"></i>
                                    </div>
                                    <input type="email" id="mailReg" name="mailReg" placeholder="Mail" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Mail'" required class="single-input" runat="server">
                                </div>
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-phone-square" aria-hidden="true"></i>
                                    </div>
                                    <input type="tel" id="telReg" name="telReg" placeholder="Telefono Prefisso Escluso" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Telefono Prefisso Escluso'" required class="single-input" runat="server">
                                </div>
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-user" aria-hidden="true"></i>
                                    </div>
                                    <input type="text" id="usernameReg" name="usernameReg" placeholder="Username" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Username'" required class="single-input" runat="server">
                                </div>
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-lock" aria-hidden="true"></i>
                                    </div>
                                    <input type="password" id="pwdReg" name="pwdReg" placeholder="Password" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Password'" aria-describedby="descPwdReg" required class="single-input" runat="server">
                                    <small id="descPwdReg" class="form-text text-muted">La Password deve contenere almeno 8 caratteri tra cui almeno una maiuscola, una minuscola, un numero e un carattere tra !@#$%^&*
                                    </small>
                                </div>
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-map-marker" aria-hidden="true"></i>
                                    </div>
                                    <input type="text" id="viaReg" name="viaReg" placeholder="Via" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Via'" required class="single-input" runat="server">
                                </div>
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-map-marker" aria-hidden="true"></i>
                                    </div>
                                    <input type="text" id="civicoReg" name="civicoReg" placeholder="Civico" onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Civico'" required class="single-input" runat="server">
                                </div>
                                <div class="input-group-icon mt-10">
                                    <div class="icon">
                                        <i class="fa fa-building" aria-hidden="true"></i>
                                    </div>
                                    <div>
                                        <select id="lstCittaReg" title="Città Utente" runat="server" class="single-input" required>
                                        </select>
                                    </div>
                                </div>
                                <div id="msgReg" class="msg" runat="server"></div>
                                <br>
                            </div>
                            <div class="col-sm-6 col-md-6 col-lg-6 mx-auto">
                                <div class="form-group">
                                    <asp:Button ID="btnRegistrati" class="genric-btn primary circle btn-block" runat="server" Text="Registrati" OnClick="btnRegistrati_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--================End Home Banner Area =================-->


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
    <script src="vendors/isotope/imagesloaded.pkgd.min.js"></script>
    <script src="vendors/isotope/isotope-min.js"></script>
    <script src="vendors/owl-carousel/owl.carousel.min.js"></script>
    <script src="js/jquery.ajaxchimp.min.js"></script>
    <script src="vendors/counter-up/jquery.waypoints.min.js"></script>
    <script src="vendors/counter-up/jquery.counterup.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.12/dist/js/bootstrap-select.min.js"></script>
    <script src="js/mail-script.js"></script>
    <script src="js/registrati.js"></script>
</body>

</html>
