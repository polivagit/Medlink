<?php
    //login
    $username="pau";
    $password="admin";
    $fields=array("user"=>"pgarcia1","pass"=>"4720A");
    $fields_string=http_build_query($fields);
    $url="http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/login";
    $ch=curl_init();
    curl_setopt($ch, CURLOPT_URL,$url);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    curl_setopt($ch, CURLOPT_HEADER, 0);
    curl_setopt($ch, CURLOPT_USERPWD, $username . ":" . $password);
    curl_setopt($ch, CURLOPT_POST,1);
    curl_setopt($ch, CURLOPT_POSTFIELDS,$fields_string);

    $data = curl_exec($ch);
    curl_close($ch);
    var_dump($data);

    //treatments
    // $username="pau";
    // $password="admin";
    // $fields=array("id"=>"1");
    // $fields_string=http_build_query($fields);
    // $url="http://169.254.30.133/Medlink/WEB_MedlinkApp/Server/index.php/apix/Request/treatment";
    // $ch=curl_init();
    // curl_setopt($ch, CURLOPT_URL,$url);
    // curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
    // curl_setopt($ch, CURLOPT_HEADER, 0);
    // curl_setopt($ch, CURLOPT_USERPWD, $username . ":" . $password);
    // curl_setopt($ch, CURLOPT_POST,1);
    // curl_setopt($ch, CURLOPT_POSTFIELDS,$fields_string);

    // $data = curl_exec($ch);
    // curl_close($ch);
    // var_dump($data);

?>