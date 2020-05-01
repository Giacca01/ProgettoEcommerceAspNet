<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dettaglioProdotto.aspx.cs" Inherits="ProgettoEcommerce.single_product" %>

<!DOCTYPE html>
<html lang="en">
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
                <div class="container">
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
                        <div
                            class="collapse navbar-collapse offset w-100"
                            id="navbarSupportedContent">
                            <div class="row w-100 mr-0">
                                <div class="col-lg-7 pr-0">
                                    <ul class="nav navbar-nav center_nav pull-right">
                                        <li class="nav-item active">
                                            <a href="dettaglioProdotto.aspx" class="nav-link dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true"
                                                aria-expanded="false" runat="server">Dettaglio Prodotto</a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="col-lg-5 pr-0">
                                    <ul id="navUtenteCarrrello" class="nav navbar-nav navbar-right right_nav pull-right" runat="server">
                                        <li class='nav-item submenu dropdown'>
                                            <a href='#' class='icons dropdown-toggle' data-toggle='dropdown' role="button" aria-haspopup='true'><i class='ti-user' aria-hidden='true'></i></a>
                                            <ul id="dropDownLogout" class='dropdown-menu' runat="server">
                                                <li class='nav-item'></li>
                                            </ul>
                                        </li>
                                        <li class='nav-item'>
                                            <a href='#' class='icons'><i class='ti-shopping-cart' aria-hidden='true'></i></a>
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
                            <h2>Dettaglio Prodotto</h2>
                        </div>
                        <div class="page_link">
                            <a href="index.aspx">Home</a>
                            <a id="linkCurrentPage" runat="server">Dettaglio Prodotto</a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--================End Home Banner Area =================-->

        <!--================Single Product Area =================-->
        <div id="contDettaglioProdotto" runat="server">
            <div class="product_image_area">
                <div class="container">
                    <div class="row s_product_inner">
                        <div class="col-lg-6">
                            <div class="s_product_img" id="contImgProd" runat="server">
                            </div>
                        </div>
                        <div class="col-lg-5 offset-lg-1">
                            <div class="s_product_text" id="contDetProd" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--================End Single Product Area =================-->

            <!--================Product Description Area =================-->
            <section class="product_description_area">
                <div class="container">
                    <ul class="nav nav-tabs" id="myTab" role="tablist">
                        <li class="nav-item">
                            <a
                                class="nav-link active show"
                                id="home-tab"
                                data-toggle="tab"
                                href="#home"
                                role="tab"
                                aria-controls="home"
                                aria-selected="true">Descrizione</a>
                        </li>
                    </ul>
                    <div class="tab-content" id="myTabContent">
                        <div
                            class="tab-pane active show"
                            id="home"
                            role="tabpanel"
                            aria-labelledby="home-tab">
                            <p id="descProdotto" runat="server">
                            </p>
                        </div>
                    </div>
                </div>
            </section>
            <!--================End Product Description Area =================-->
        </div>

        <div id="contMsgNoProd" runat="server">
            <section class="product_image_area">
                <div class="container">
                <div class="row">
                    <div class="col-sm-12 col-lg-6 col-md-6 mx-auto">
                        <h2 id="contMsgErroreCreazioneDetProd" runat="server"></h2>
                    </div>
                </div>
            </div>
            </section>
            <br />
            <br />
        </div>

        <!--================ start footer Area  =================-->
        <footer class="footer-area section_gap">
            <div class="container">
                <div class="row">
                    <div class="col-lg-2 col-md-6 single-footer-widget">
                        <h4>Top Products</h4>
                        <ul>
                            <li><a href="#">Managed Website</a></li>
                            <li><a href="#">Manage Reputation</a></li>
                            <li><a href="#">Power Tools</a></li>
                            <li><a href="#">Marketing Service</a></li>
                        </ul>
                    </div>
                    <div class="col-lg-2 col-md-6 single-footer-widget">
                        <h4>Quick Links</h4>
                        <ul>
                            <li><a href="#">Jobs</a></li>
                            <li><a href="#">Brand Assets</a></li>
                            <li><a href="#">Investor Relations</a></li>
                            <li><a href="#">Terms of Service</a></li>
                        </ul>
                    </div>
                    <div class="col-lg-2 col-md-6 single-footer-widget">
                        <h4>Features</h4>
                        <ul>
                            <li><a href="#">Jobs</a></li>
                            <li><a href="#">Brand Assets</a></li>
                            <li><a href="#">Investor Relations</a></li>
                            <li><a href="#">Terms of Service</a></li>
                        </ul>
                    </div>
                    <div class="col-lg-2 col-md-6 single-footer-widget">
                        <h4>Resources</h4>
                        <ul>
                            <li><a href="#">Guides</a></li>
                            <li><a href="#">Research</a></li>
                            <li><a href="#">Experts</a></li>
                            <li><a href="#">Agencies</a></li>
                        </ul>
                    </div>
                    <div class="col-lg-4 col-md-6 single-footer-widget">
                        <h4>Newsletter</h4>
                        <p>You can trust us. we only send promo offers,</p>
                        <div class="form-wrap" id="mc_embed_signup">
                            <form target="_blank" action="https://spondonit.us12.list-manage.com/subscribe/post?u=1462626880ade1ac87bd9c93a&amp;id=92a4423d01"
                                method="get" class="form-inline">
                                <input class="form-control" name="EMAIL" placeholder="Your Email Address" onfocus="this.placeholder = ''"
                                    onblur="this.placeholder = 'Your Email Address '" required="" type="email">
                                <button class="click-btn btn btn-default">Subscribe</button>
                                <div style="position: absolute; left: -5000px;">
                                    <input name="b_36c4fd991d266f23781ded980_aefe40901a" tabindex="-1" value="" type="text">
                                </div>

                                <div class="info"></div>
                            </form>
                        </div>
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

