function fnEdit(No) {
    var title = "";
    if (No == 0) {
        title = "Ekle";
    }
    else {
        title = "Düzenle";
    }
    $("#dvGenel").load("/Fatura/GenelPartial/" + No + "?partial=_FaturaEdit");
    $('#wGenel').data('kendoWindow').setOptions({ width: 800, height: 420 }); $('#wGenel').data('kendoWindow').title(title).center().open();
}

function fnList() {
    $("#dvListe").load("/Fatura/GenelPartial/" + $("#txtBul").val().toString().replace(' ', '_') + "?partial=_FaturaList");
}
function fnDelete(ID) {
    var title = "Silme";
    $("#dvGenel").load("/Fatura/GenelPartial/" + ID + "?partial=_KayitDelete");
    $('#wGenel').data('kendoWindow').setOptions({ width: 250, height: 120 }); $('#wGenel').data('kendoWindow').title(title).center().open();

}
function fnFind(e) {
    if (e.keyCode == 13) {
        fnList();
    }
}
function fnUrunEkle(No) {
    $("#dvfChild").load("/Fatura/GenelPartial/" + No + "?partial=_fDetayEdit");
    $('#wfChild').data('kendoWindow').setOptions({ width: 400, height: 200 });
    $('#wfChild').data('kendoWindow').title("Ekle").center().open();
}
function fnfDetayDelete(No) {
    $("#dvfChild").load("/Fatura/GenelPartial/" + No + "?partial=_fDetayDelete");
    $('#wfChild').data('kendoWindow').setOptions({ width: 300, height:150 });
    $('#wfChild').data('kendoWindow').title("Sil").center().open();
}
//function Close() {
//    $('#wGenel').data('kendoWindow').close();
//}
//function Sonuc(result) {
//    if (result.Sonuc == true) {
//        //fnList();
//    }
//    else {
//                msgAlert('Hata Mesajı', result.Cevap);
//    }
//}
//function Hata() {
//    msgAlert('Hata Mesajı','İşleminiz Esnasında Bir Sorun Oluşmuştur');
//}