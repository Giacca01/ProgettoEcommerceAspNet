<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reimpostaPwd.aspx.cs" Inherits="ProgettoEcommerce.reimpostaPwd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.12/dist/css/bootstrap-select.min.css" />
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
                <div class="container" style="max-width:100%">
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
                                            <a href="#" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true"
                                                aria-expanded="false">Elenco Prodotti</a>
                                        </li>
                                        <li class="nav-item">
                                            <a href="#" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true"
                                                aria-expanded="false">Registrati</a>
                                        </li>
                                        <li class="nav-item">
                                            <a href="#" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true"
                                                aria-expanded="false">Login</a>
                                        </li>
                                        <li class="nav-item active">
                                            <a href="reimpostaPwd.aspx" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true"
                                                aria-expanded="false">Reimposta Password</a>
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

        <!--================ Sezione Reimpostazione Password =================-->
        <section class="section_gap">
            <div class="banner_inner d-flex align-items-center">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8 mx-auto">
                            <div class="main_title">
                                <h2><span>Reimposta Password</span></h2>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-md-6 col-lg-6 mx-auto">
                                    <div class="input-group-icon mt-10">
                                        <div class="icon">
                                            <i class="fa fa-user" aria-hidden="true"></i>
                                        </div>
                                        <input type="text" id="usernameReimpPwd" name="usernameReimpPwd" placeholder="Username"
                                            onfocus="this.placeholder = ''; this.classList.remove('alert-danger');" onblur="this.placeholder = 'Username'"
                                            class="single-input" runat="server" required="required" />
                                    </div>
                                    <div class="input-group-icon mt-10">
                                        <div class="icon">
                                            <i class="fa fa-envelope" aria-hidden="true"></i>
                                        </div>
                                        <input type="email" id="emailReimpPwd" name="emailReimpPwd" placeholder="Email"
                                            onfocus="this.placeholder = '';this.classList.remove('alert-danger');" onblur="this.placeholder = 'Email'" required="required"
                                            class="single-input" runat="server" />
                                    </div>
                                    <div class="input-group-icon mt-10">
                                        <div class="icon">
                                            <i class="fa fa-phone" aria-hidden="true"></i>
                                        </div>
                                        <input type="tel" id="telReimpPwd" name="telReimpPwd" placeholder="Telefono"
                                            onfocus="this.placeholder = '';this.classList.remove('alert-danger');" onblur="this.placeholder = 'Telefono'" required="required"
                                            class="single-input" runat="server" />
                                    </div>
                                    <div class="input-group-icon mt-10">
                                        <div class="icon">
                                            <i class="fa fa-lock" aria-hidden="true"></i>
                                        </div>
                                        <input type="password" id="pwdReimpPwd" name="pwdReimpPwd" placeholder="Nuova Password"
                                            onfocus="this.placeholder = '';this.classList.remove('alert-danger');" onblur="this.placeholder = 'Nuova Password'" required="required"
                                            class="single-input" runat="server" />
                                    </div>
                                    <div class="input-group-icon mt-10">
                                        <div class="icon">
                                            <i class="fa fa-tag"></i>
                                        </div>
                                        <div>
                                            <select id="lstTipoUtenteReimpPwd" title="Tipo Utente" runat="server" class="single-input form-control" required="required">
                                                <option value="admin">Admin</option>
                                                <option value="cliente">Cliente</option>
                                                <option value="fornitore">Fornitore</option>
                                            </select>
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12 col-md-7 col-lg-7 mx-auto">
                                    <div id="msgReimpPwd" role="alert" style="text-align: center;" runat="server"></div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6 col-md-6 col-lg-6 mx-auto">
                                    <div class="form-group">
                                        <asp:Button ID="btnReimpPwd" class="genric-btn primary circle btn-block" runat="server" Text="Reimposta" OnClick="btnReimpPwd_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--================ Fine Sezione Reimpostazione Password =================-->


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
    <script src="js/reimpostaPwd.js"></script>
</body>
</html>
