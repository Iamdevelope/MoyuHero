
package xbean;

public interface ArtifactColumn extends xdb.Bean {
	public ArtifactColumn copy(); // deep clone
	public ArtifactColumn toData(); // a Data instance
	public ArtifactColumn toBean(); // a Bean instance
	public ArtifactColumn toDataIf(); // a Data instance If need. else return this
	public ArtifactColumn toBeanIf(); // a Bean instance If need. else return this

	public java.util.Map<Integer, xbean.Artifact> getArtifacts(); // 
	public java.util.Map<Integer, xbean.Artifact> getArtifactsAsData(); // 

}
