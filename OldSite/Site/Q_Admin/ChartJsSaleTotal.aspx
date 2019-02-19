<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/MasterPageH5.master" AutoEventWireup="true" CodeFile="ChartJsSaleTotal.aspx.cs" Inherits="Q_Admin_ChartJsSaleTotal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="/js_css/ChartJs/Chart.min.js"></script>
    <script src="/js_css/ChartJs/Chart.bundle.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div id="myChart1Title"></div>
        <canvas id="myChart1" width="800" height="400"></canvas>
    </div>
    <div>
        <div id="myChart2Title"></div>
        <canvas id="myChart2" width="800" height="400"></canvas>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <script>
        $(function () {

            var defaultConfigs = {

                //Boolean - If we show the scale above the chart data			
                scaleOverlay: false,

                //Boolean - If we want to override with a hard coded scale
                scaleOverride: false,

                //** Required if scaleOverride is true **
                //Number - The number of steps in a hard coded scale
                scaleSteps: null,
                //Number - The value jump in the hard coded scale
                scaleStepWidth: null,
                //Number - The scale starting value
                scaleStartValue: null,

                //String - Colour of the scale line	
                scaleLineColor: "rgba(0,0,0,.1)",

                //Number - Pixel width of the scale line	
                scaleLineWidth: 1,

                //Boolean - Whether to show labels on the scale	
                scaleShowLabels: false,

                //Interpolated JS string - can access value
            

                //String - Scale label font declaration for the scale label
                scaleFontFamily: "'Arial'",

                //Number - Scale label font size in pixels	
                scaleFontSize: 12,

                //String - Scale label font weight style	
                scaleFontStyle: "normal",

                //String - Scale label font colour	
                scaleFontColor: "#666",

                ///Boolean - Whether grid lines are shown across the chart
                scaleShowGridLines: true,

                //String - Colour of the grid lines
                scaleGridLineColor: "rgba(0,0,0,.05)",

                //Number - Width of the grid lines
                scaleGridLineWidth: 1,

                //Boolean - If there is a stroke on each bar	
                barShowStroke: true,

                //Number - Pixel width of the bar stroke	
                barStrokeWidth: 2,

                //Number - Spacing between each of the X value sets
                barValueSpacing: 5,

                //Number - Spacing between data sets within X values
                barDatasetSpacing: 1,

                //Boolean - Whether to animate the chart
                animation: true,

                //Number - Number of animation steps
                animationSteps: 60,

                //String - Animation easing effect
                animationEasing: "easeOutQuart",

                //Function - Fires when the animation is complete
                onAnimationComplete: null

            };
            $.getJSON("/q_admin/OpenFlashChartData.aspx", { cmd: 'GetStat360Days' }, function (response) {
                //var data1 = {
                //    labels: ["January", "February", "March", "April", "May", "June", "July"],
                //    datasets: [
                //        {
                //            fillColor: "rgba(220,220,220,0.5)",
                //            strokeColor: "rgba(220,220,220,1)",
                //            data: [65, 59, 90, 81, 56, 55, 40]
                //        },
                //        {
                //            fillColor: "rgba(151,187,205,0.5)",
                //            strokeColor: "rgba(151,187,205,1)",
                //            data: [28, 48, 40, 19, 96, 27, 100]
                //        }
                //    ]
                //}

                var ctx1 = document.getElementById("myChart1").getContext("2d");
               // var myNewChart1 = new Chart(ctx1).Bar(data1);
                var myNewChart1 = new Chart(ctx1, {
                    type: 'bar',
                    data: response.data,
                    options: defaultConfigs
                });
                //var ctx2 = document.getElementById("myChart2").getContext("2d");
                //var myNewChart2 = new Chart(ctx2).PolarArea(data2);
            })

        });
    </script>
</asp:Content>

