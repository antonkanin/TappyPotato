<?php
	require_once __DIR__ . '/vendor/autoload.php';

	$fb = new \Facebook\Facebook([
		'app_id' => '219403772103872',
		'app_secret' => 'fd98889a08a279f85755b8c2d7727f5c',
		'default_graph_version' => 'v2.10',
		//'default_access_token' => '{access-token}', // optional
	]);
	
	try {
		$response = $fb->get('/me?fields=first_name', $_POST["access_token"]);
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
	
	$sql = "INSERT INTO score_board (player_id, player_name, score, date_created) "
		."VALUES ('".$fb_user_id."', '".$first_name."', '".$_POST["score"]."', now())";
		
	echo $sql;
	if($conn->query($sql) === TRUE)	{
		echo "Records inserted successfully.";
	} else {
		echo "ERROR: Could not able to execute $sql. " . $conn->error;
	}
	
	$conn->close();
?>
