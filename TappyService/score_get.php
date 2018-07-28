<?php
	$db_settings = parse_ini_file("../private/config.ini");

	$conn = new mysqli($db_settings['servername'], $db_settings['username'], $db_settings['password'], $db_settings['db_name']);

	// Check connection
	if($conn->connect_error) {
		die("ERROR: Could not connect. " . $conn->connect_error);
	}

	$sql = "select * from score_board where number in
		(select number from (select max(score), number from score_board group by player_id) as t)";
	
	$result = $conn->query($sql);

	if (!$result) {
		trigger_error("Invalid query: " . $conn->error);
	}

	$resultArray = array();

	if ($result->num_rows > 0) {
		while($row = $result->fetch_assoc()) {
			$resultArray[] = $row;
		}
		echo json_encode($resultArray);
	}
	else {
		echo "no records in the database";
	}

	$conn->close();
?>