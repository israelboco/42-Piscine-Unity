using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class waypointChecks : MonoBehaviour {

	public List<waypointChecks> links = new List<waypointChecks>();

	public Vector2 position { get; private set; }

	void OnDrawGizmos () {
		Gizmos.DrawIcon(transform.position, "map.png", false);
		foreach (waypointChecks link in links) {
			if (link) {
				Vector3 target = link.transform.position;
				Gizmos.color = Color.green;
				Gizmos.DrawLine(transform.position, target);
			}
		}
	}

	void Awake () {
		position = (Vector2)transform.position;
	}

	void Start () {
		foreach (waypointChecks link in links) {
			if (link != null && link.links.Contains(this) == false) {
				link.links.Add(this);
			}
		}
		// links.RemoveAll (isNull);
		// gameManager.instance.waypointCheckswaypointCheckss.Add (this);
	}

	private bool isNull (waypointChecks link) {
		return (link == null);
	}
	
	public List<Vector2> getPathFrom (waypointChecks start, bool fast, List<waypointChecks> prev) {
		if (prev.Contains (this) || (!fast && prev.Count > 10)) {
			return null;
		}
		prev = new List<waypointChecks> (prev);
		prev.Add (this);
		List<Vector2> path = null;
		if (this == start) {
			path = new List<Vector2> ();
			path.Add (this.position);
			return path;
		}
		if (fast) {
			foreach (waypointChecks link in links) {
				path = link.getPathFrom (start, fast, prev);
				if (path != null) {
					path.Add (this.position);
					return path;
				}
			}
		} else {
			List<Vector2>[] paths = new List<Vector2>[links.Count];
			int i = 0;
			foreach (waypointChecks link in links) {
				paths[i] = link.getPathFrom (start, fast, prev);
				i++;
			}
			path = null;
			int pathCount = int.MaxValue;
			foreach (List<Vector2> p in paths) {
				if (p != null && p.Count < pathCount) {
					path = p;
					pathCount = p.Count;
				}
			}
			if (path != null)
				path.Add (this.position);
		}
		return path;
	}
}
