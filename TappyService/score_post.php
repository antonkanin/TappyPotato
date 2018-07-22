<?php
	require_once __DIR__ . '/vendor/autoload.php'

	$fb = new \Facebook\Facebook([
		'app_id' => '{app-id}',
		'app_secret' => '{app-secret}',
		'default_graph_version' => 'v2.10',
		//'default_access_token' => '{access-token}', // optional
	]);

	$conn = new mysqli("localhost", "root", "", "tappy_potato");
	
	if($conn->connect_error)
	{
		die("ERROR: Could not connect. " . $conn->connect_error);
	}
	
	$access_token = $_POST["access_token"];
	$user_details = "https://graph.facebook.com/me?access_token=" .$access_token;

	
	$response = file_get_contents($user_details);
	var_dump($response);
	//echo $response['name'];
	echo '</br></br>';
	$response = json_decode($response);
	echo $response->{'name'};
	//print_r($response)
	//var_dump($response);
 
	/*
	$sql = "INSERT INTO score_board (player, score, date) VALUES ('".$_POST["name"]."','".$_POST["score"]."', now())";
	echo $sql;
	if($conn->query($sql) === TRUE)
	{
		echo "Records inserted successfully.";
	} 
	else
	{
		echo "ERROR: Could not able to execute $sql. " . $conn->error;
	}
	*/
	$conn->close();
?>
