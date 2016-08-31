
// move status
public enum MovePosition {
	Stay =0,
	Left,
	Right
}

// bounce status
public enum Bouncy {
	Ready=0,
	Down,
	Up,
	Not,
	Wait,
	Twin,
	ride,
	warp,
	warpexit,
	stun
}

public enum PlayerCC {
	not=0,
	heavy,
	high,
	reverse,
	bug,
	horizon,
	riding
}

// game status
public enum GameSet {
	play =0,
	win,
	lose
}

// fish
public enum fishMove {
	wait=0,
	up,
	down,
	bite
}

public enum objMove {
	wait=0,
	up,
	down,
	not
}

//crocodile
public enum crocodileMove {
	normal=0,
	angly
}

public enum objectStyle {
	ground=0,
	breakground,
	fish,
	crocodile
}

public enum elephantStatus {
	stay = 0,
	wait,
	att,
	end,
	not
}

public enum hawkStatus {
	stay = 0,
	wait,
	att,
	end,
	not
}