
//console.log(res)
//if (res[1] + "/" + res[2] != "VideoManage/Index")
//{
//    var timeout;
//    document.onmousemove = function () {
//        clearTimeout(timeout);
//        timeout = setTimeout(function () {
//            var r = window.confirm("Session Time Out, Please Log In");
//            if (r == true) {
//                window.location = '/Account/Logout';
//            } else {
//                window.location = '/Account/Logout';
//            }
//        }, 10000);
//    }
//}
//else if (res[1] + "/" + res[2] != "VideoConference/LiveClass")
//{
//    var timeout;
//    document.onmousemove = function () {
//        clearTimeout(timeout);
//        timeout = setTimeout(function () {
//            var r = window.confirm("Session Time Out, Please Log In");
//            if (r == true) {
//                window.location = '/Account/Logout';
//            } else {
//                window.location = '/Account/Logout';
//            }
            
//        }, 10000);
//    }
//}
//else if (res[1] + "/" + res[2] != "VideoConference/ChapterWiseVideo") {
//    var timeout;
//    document.onmousemove = function () {
//        clearTimeout(timeout);
//        timeout = setTimeout(function () {
//            var r = window.confirm("Session Time Out, Please Log In");
//            if (r == true) {
//                window.location = '/Account/Logout';
//            } else {
//                window.location = '/Account/Logout';
//            }

//        }, 10000);
//    }
//}
//else
//{
//    console.log("We are in Video Page")
//}

var currentpath = window.location.pathname;
var res = currentpath.split("/");
var timeout;
document.onmousemove = function () {
    clearTimeout(timeout);
    timeout = setTimeout(function () {
        if (res[1] + "/" + res[2] != "VideoConference/ChapterWiseVideo") {
            var r = window.confirm("Session Time Out, Please Log In");
            if (r == true) {
                window.location = '/Account/Logout';
            } else {
                window.location = '/Account/Logout';
            }
        }
        else {
            console.log("We are in Video Page")
        }
    }, 1000 * 60 * 2);
}