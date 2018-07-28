<?php
	require_once __DIR__ . '/vendor/autoload.php';

	$key = base64_decode($_POST["key"]);
	$iv = base64_decode($_POST["iv"]);
	$access_token_raw = openssl_decrypt($_POST["access_token"], "AES-128-CBC", $key, $options=0, $iv);
	

	$fb = new \Facebook\Facebook([
		'app_id' => '219403772103872',
		'app_secret' => 'fd98889a08a279f85755b8c2d7727f5c',
		'default_graph_version' => 'v2.10',
	]);
	
	try {
		$response = $fb->get('/me?fields=first_name', $access_token_raw);
	} catch(\Facebook\Exceptions\FacebookResponseException $e) {
		echo 'Graph returned an error: ' . $e->getMessage();
		exit;
	} catch(\Facebook\Exceptions\FacebookSDKException $e) {
		echo 'Facebook SDK returned an error: ' . $e->getMessage();
		exit;
	}

	$user = $response->getGraphUser();
	$fb_user_id = $user['id'];
	$first_name = $user['first_name'];

	
	////////////////////////////////////////////////////////////////////////////////////
	// Connecting to the database. Make sure you have ../private/config.ini file:
	// 	[database]
	// 	servername=<servername>
	// 	username=<user>
	// 	password=<password>
	// 	db_name=<database name>

	$db_settings = parse_ini_file("../private/config.ini");

	$conn = new mysqli($db_settings['servername'], $db_settings['username'], $db_settings['password'], $db_settings['db_name']);
	
	if($conn->connect_error) {
		die("ERROR: Could not connect. " . $conn->connect_error);
	}
	
	$sql = "INSERT INTO score_board (player_id, player_name, score, death_position, date_created) "
		."VALUES ('".$fb_user_id."', '".$first_name."', '".$_POST["score"]."','".$_POST["position_x"]."', now())";
		
	echo $sql;
	if($conn->query($sql) === TRUE)	{
		echo "Records inserted successfully.";
	} else {
		echo "ERROR: Could not able to execute $sql. " . $conn->error;
	}
	
	$conn->close();
?>