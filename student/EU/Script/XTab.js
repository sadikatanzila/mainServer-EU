function XTabOnload(){
    var tabCurrentButtonId = GetCurrentXTabButtonId();
    var tabCurrentButton = GetElementByTagId(tabCurrentButtonId,"SPAN");
    if(tabCurrentButtonId != null && tabCurrentButton != null){
        OnClickShowXTab(tabCurrentButton);
    }
}

function GetCurrentXTabButtonId(){
    var hf = GetElementByTagId("hfCurrentXtabButton","INPUT");
    
    if(hf != null){
        return hf.value;
    }
    
    return null;
}

function SetCurrentXTabButtonId(tabButtonId){
    var hf = GetElementByTagId("hfCurrentXtabButton","INPUT");
    
    if(hf != null){
        hf.value = tabButtonId;
    }
}

function OnClickShowXTab(tabClickedButton){
    debugger;
    var tabButtonRow = tabClickedButton.parentNode.parentNode;
    //var tabGlowRow = tabButtonRow.previousSibling;
    var tabPageRow = tabButtonRow.nextSibling;
    
    for(i=0; i < tabPageRow.childNodes[0].childNodes.length; i++){
        var j = i; //(i * 2) + 1; // generate odd number series
        //var tabGlowCell = tabGlowRow.childNodes[j];
        var tabButton = tabButtonRow.childNodes[j].childNodes[0];
        var tabButtonCell = tabButtonRow.childNodes[j];
        var tabPage = tabPageRow.childNodes[0].childNodes[i];
                
        if(tabClickedButton.id == tabButton.id){ 
            //tabGlowCell.className = "XTabActiveGlowCell";
            tabButton.className = "XTabActiveButton";
            tabButtonCell.className = "XTabActiveButtonCell";
            //tabPage.className = "XTabAactivePanel"; 
            tabPage.className = "XTabActivePanel";                          
        }else{
            //tabGlowCell.className = "XTabInactiveGlowCell";
            tabButton.className = "XTabInactiveButton";
            tabButtonCell.className = "XTabInactiveButtonCell";
            tabPage.className = "XTabInactivePanel";
        }
     
    }        
    
    // save current tab page button
    SetCurrentXTabButtonId(tabClickedButton.id);
}


