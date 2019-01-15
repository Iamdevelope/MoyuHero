package xtable;

// typed table access point
public class Artifactcolumns {
	Artifactcolumns() {
	}

	public static xbean.ArtifactColumn get(Long key) {
		return _Tables_.getInstance().artifactcolumns.get(key);
	}

	public static xbean.ArtifactColumn get(Long key, xbean.ArtifactColumn value) {
		return _Tables_.getInstance().artifactcolumns.get(key, value);
	}

	public static void insert(Long key, xbean.ArtifactColumn value) {
		_Tables_.getInstance().artifactcolumns.insert(key, value);
	}

	public static void delete(Long key) {
		_Tables_.getInstance().artifactcolumns.delete(key);
	}

	public static boolean add(Long key, xbean.ArtifactColumn value) {
		return _Tables_.getInstance().artifactcolumns.add(key, value);
	}

	public static boolean remove(Long key) {
		return _Tables_.getInstance().artifactcolumns.remove(key);
	}

	public static xdb.TTableCache<Long, xbean.ArtifactColumn> getCache() {
		return _Tables_.getInstance().artifactcolumns.getCache();
	}

	public static xdb.TTable<Long, xbean.ArtifactColumn> getTable() {
		return _Tables_.getInstance().artifactcolumns;
	}

	public static xbean.ArtifactColumn select(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ArtifactColumn, xbean.ArtifactColumn>() {
			public xbean.ArtifactColumn get(xbean.ArtifactColumn v) { return v.toData(); }
		});
	}

	public static java.util.Map<Integer, xbean.Artifact> selectArtifacts(Long key) {
		return getTable().select(key, new xdb.TField<xbean.ArtifactColumn, java.util.Map<Integer, xbean.Artifact>>() {
				public java.util.Map<Integer, xbean.Artifact> get(xbean.ArtifactColumn v) { return v.getArtifactsAsData(); }
			});
	}

}
